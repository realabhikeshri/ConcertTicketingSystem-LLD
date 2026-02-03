using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Models
{
    public class SeatHold
    {
        public string Id { get; }
        public string ConcertId { get; }
        public string CustomerId { get; }
        public List<string> SeatIds { get; }
        public DateTime ExpiryTime { get; }

        public SeatHold(string id, string concertId, string customerId, List<string> seatIds, DateTime expiryTime)
        {
            Id = id;
            ConcertId = concertId;
            CustomerId = customerId;
            SeatIds = seatIds;
            ExpiryTime = expiryTime;
        }

        public bool IsExpired() => DateTime.UtcNow > ExpiryTime;
    }
}
