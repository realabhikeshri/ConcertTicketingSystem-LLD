using ConcertTicketingSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Strategies.Pricing
{
    public interface IPricingStrategy
    {
        decimal CalculateTotalPrice(IEnumerable<Seat> seats);
    }
}
