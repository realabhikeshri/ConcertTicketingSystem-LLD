using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Models
{
    public class Venue
    {
        public string Id { get; }
        public string Name { get; }
        public string Location { get; }

        private readonly ConcurrentDictionary<string, Seat> _seats = new();

        public Venue(string id, string name, string location)
        {
            Id = id;
            Name = name;
            Location = location;
        }

        public ICollection<Seat> Seats => _seats.Values;

        public void AddSeat(Seat seat)
        {
            _seats[seat.Id] = seat;
        }
    }
}
