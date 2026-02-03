using ConcertTicketingSystem_LLD.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Repositories
{
    public class InMemoryBookingRepository : IBookingRepository
    {
        private readonly ConcurrentDictionary<string, Booking> _bookings = new();

        public void Save(Booking booking)
        {
            _bookings[booking.Id] = booking;
        }

        public Booking? GetById(string bookingId)
        {
            _bookings.TryGetValue(bookingId, out var booking);
            return booking;
        }

        public void Update(Booking booking)
        {
            _bookings[booking.Id] = booking;
        }
    }
}
