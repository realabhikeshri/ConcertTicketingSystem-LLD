using ConcertTicketingSystem_LLD.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Models
{
    public class Booking
    {
        public string Id { get; }
        public string ConcertId { get; }
        public Customer Customer { get; }
        public List<Seat> Seats { get; }
        public decimal TotalAmount { get; }
        public BookingStatus Status { get; private set; }
        public DateTime CreatedAt { get; }

        public Booking(string id, string concertId, Customer customer, List<Seat> seats, decimal totalAmount)
        {
            Id = id;
            ConcertId = concertId;
            Customer = customer;
            Seats = seats;
            TotalAmount = totalAmount;
            Status = BookingStatus.Confirmed;
            CreatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            Status = BookingStatus.Cancelled;
        }
    }
}
