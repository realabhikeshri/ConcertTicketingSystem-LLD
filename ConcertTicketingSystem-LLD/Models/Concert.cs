using ConcertTicketingSystem_LLD.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Models
{
    public class Concert
    {
        public string Id { get; }
        public string Name { get; }
        public DateTime StartTime { get; }
        public Venue Venue { get; }

        public Concert(string id, string name, DateTime startTime, Venue venue)
        {
            Id = id;
            Name = name;
            StartTime = startTime;
            Venue = venue;
        }

        public IEnumerable<Seat> GetAvailableSeats()
        {
            return Venue.Seats.Where(s => s.Status == SeatStatus.Available);
        }

        public Seat? GetSeatById(string seatId)
        {
            return Venue.Seats.FirstOrDefault(s => s.Id == seatId);
        }
    }
}
