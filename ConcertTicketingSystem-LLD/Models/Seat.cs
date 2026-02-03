using ConcertTicketingSystem_LLD.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Models
{
    public class Seat
    {
        private readonly object _lock = new();

        public string Id { get; }
        public int Row { get; }
        public int Number { get; }
        public decimal Price { get; }
        public SeatStatus Status { get; private set; }

        public Seat(string id, int row, int number, decimal price)
        {
            Id = id;
            Row = row;
            Number = number;
            Price = price;
            Status = SeatStatus.Available;
        }

        public bool TryHold()
        {
            lock (_lock)
            {
                if (Status != SeatStatus.Available) return false;
                Status = SeatStatus.Held;
                return true;
            }
        }

        public bool TryBook()
        {
            lock (_lock)
            {
                if (Status != SeatStatus.Held) return false;
                Status = SeatStatus.Booked;
                return true;
            }
        }

        public void Release()
        {
            lock (_lock)
            {
                if (Status == SeatStatus.Held)
                {
                    Status = SeatStatus.Available;
                }
            }
        }
    }
}
