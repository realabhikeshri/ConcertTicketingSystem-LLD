using ConcertTicketingSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Strategies.Allocation
{
    // Example heuristic: lower row first, then lower seat number
    public class BestAvailableSeatStrategy : ISeatAllocationStrategy
    {
        public List<Seat> AllocateSeats(IEnumerable<Seat> availableSeats, int count)
        {
            return availableSeats
                .OrderBy(s => s.Row)
                .ThenBy(s => s.Number)
                .Take(count)
                .ToList();
        }
    }
}
