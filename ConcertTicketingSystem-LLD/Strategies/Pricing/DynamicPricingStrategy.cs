using ConcertTicketingSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Strategies.Pricing
{
    // Simple example: if more than 70% sold, add 20% surge
    public class DynamicPricingStrategy : IPricingStrategy
    {
        private readonly Func<(int sold, int total)> _inventoryStatsProvider;

        public DynamicPricingStrategy(Func<(int sold, int total)> inventoryStatsProvider)
        {
            _inventoryStatsProvider = inventoryStatsProvider;
        }

        public decimal CalculateTotalPrice(IEnumerable<Seat> seats)
        {
            var (sold, total) = _inventoryStatsProvider();
            decimal basePrice = seats.Sum(s => s.Price);

            if (total == 0) return basePrice;

            var ratio = (decimal)sold / total;
            if (ratio > 0.7m)
            {
                return basePrice * 1.2m;
            }

            return basePrice;
        }
    }
}
