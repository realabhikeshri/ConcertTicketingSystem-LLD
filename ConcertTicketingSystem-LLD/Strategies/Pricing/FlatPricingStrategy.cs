using ConcertTicketingSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Strategies.Pricing
{
    public class FlatPricingStrategy : IPricingStrategy
    {
        public decimal CalculateTotalPrice(IEnumerable<Seat> seats)
        {
            return seats.Sum(s => s.Price);
        }
    }
}
