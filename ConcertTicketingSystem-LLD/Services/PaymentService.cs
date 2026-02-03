using ConcertTicketingSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Services
{
    public class PaymentService
    {
        public Payment ProcessPayment(Booking booking)
        {
            // Mock payment – always successful in this demo
            var payment = new Payment(Guid.NewGuid().ToString(), booking.Id, booking.TotalAmount);
            payment.MarkSuccessful();
            return payment;
        }
    }
}
