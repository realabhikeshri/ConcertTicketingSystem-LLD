using ConcertTicketingSystem_LLD.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Repositories
{
    public class InMemoryConcertRepository : IConcertRepository
    {
        private readonly ConcurrentDictionary<string, Venue> _venues = new();
        private readonly ConcurrentDictionary<string, Concert> _concerts = new();

        public void AddVenue(Venue venue)
        {
            _venues[venue.Id] = venue;
        }

        public Venue? GetVenueById(string venueId)
        {
            _venues.TryGetValue(venueId, out var venue);
            return venue;
        }

        public void AddConcert(Concert concert)
        {
            _concerts[concert.Id] = concert;
        }

        public Concert? GetConcertById(string concertId)
        {
            _concerts.TryGetValue(concertId, out var concert);
            return concert;
        }
    }
}
