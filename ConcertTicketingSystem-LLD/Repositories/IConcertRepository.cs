using ConcertTicketingSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Repositories
{
    public interface IConcertRepository
    {
        void AddVenue(Venue venue);
        Venue? GetVenueById(string venueId);

        void AddConcert(Concert concert);
        Concert? GetConcertById(string concertId);
    }
}
