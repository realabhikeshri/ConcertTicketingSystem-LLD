using ConcertTicketingSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Repositories
{
    public interface IBookingRepository
    {
        void Save(Booking booking);
        Booking? GetById(string bookingId);
        void Update(Booking booking);
    }
}
