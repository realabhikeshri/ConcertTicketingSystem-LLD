using ConcertTicketingSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Strategies.Allocation
{
    public interface ISeatAllocationStrategy
    {
        List<Seat> AllocateSeats(IEnumerable<Seat> availableSeats, int count);
    }
}
