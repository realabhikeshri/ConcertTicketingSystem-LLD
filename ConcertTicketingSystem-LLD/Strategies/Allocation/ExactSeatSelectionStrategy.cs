using ConcertTicketingSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Strategies.Allocation
{
    public class ExactSeatSelectionStrategy : ISeatAllocationStrategy
    {
        private readonly HashSet<string> _requestedSeatIds;

        public ExactSeatSelectionStrategy(IEnumerable<string> seatIds)
        {
            _requestedSeatIds = new HashSet<string>(seatIds);
        }

        public List<Seat> AllocateSeats(IEnumerable<Seat> availableSeats, int count)
        {
            var selected = availableSeats.Where(s => _requestedSeatIds.Contains(s.Id)).ToList();
            if (selected.Count != _requestedSeatIds.Count)
            {
                throw new InvalidOperationException("Not all requested seats are available.");
            }
            return selected;
        }
    }
}
