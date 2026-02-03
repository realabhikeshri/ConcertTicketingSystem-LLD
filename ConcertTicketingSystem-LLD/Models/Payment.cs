using ConcertTicketingSystem_LLD.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Models
{
    public class Payment
    {
        public string Id { get; }
        public string BookingId { get; }
        public decimal Amount { get; }
        public PaymentStatus Status { get; private set; }
        public DateTime CreatedAt { get; }

        public Payment(string id, string bookingId, decimal amount)
        {
            Id = id;
            BookingId = bookingId;
            Amount = amount;
            Status = PaymentStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkSuccessful() => Status = PaymentStatus.Successful;
        public void MarkFailed() => Status = PaymentStatus.Failed;
    }
}
