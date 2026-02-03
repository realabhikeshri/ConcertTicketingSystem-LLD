using ConcertTicketingSystem_LLD.Enums;
using ConcertTicketingSystem_LLD.Exceptions;
using ConcertTicketingSystem_LLD.Models;
using ConcertTicketingSystem_LLD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public Booking ConfirmBooking(SeatHold hold)
        {
            if (hold.IsExpired())
            {
                throw new SeatNotAvailableException("Seat hold expired");
            }

            // In a real system you'd fetch concert and seats via a service
            var concertService = ConcertService.Instance;
            var (concert, seats, totalPrice) = concertService.ConvertHoldToSeatsAndPrice(hold);

            // Book the seats
            foreach (var seat in seats)
            {
                if (!seat.TryBook())
                {
                    throw new SeatNotAvailableException("Seat could not be booked");
                }
            }

            var customer = new Customer(hold.CustomerId, "Unknown", ""); // simplified
            var booking = new Booking(Guid.NewGuid().ToString(), concert.Id, customer, seats, totalPrice);
            _bookingRepository.Save(booking);
            return booking;
        }

        public Booking GetBooking(string bookingId)
        {
            return _bookingRepository.GetById(bookingId)
                ?? throw new BookingNotFoundException("Booking not found");
        }

        public void CancelBooking(string bookingId)
        {
            var booking = GetBooking(bookingId);
            if (booking.Status == BookingStatus.Cancelled) return;

            booking.Cancel();
            foreach (var seat in booking.Seats)
            {
                // In real life: decide if you can release seats after cancellation
                // For simplicity, we keep them booked in this demo.
            }

            _bookingRepository.Update(booking);
        }
    }
}
