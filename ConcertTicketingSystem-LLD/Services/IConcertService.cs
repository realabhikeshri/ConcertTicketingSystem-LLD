using ConcertTicketingSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Services
{
    public interface IConcertService
    {
        Venue CreateVenue(string name, string location, int rows, int seatsPerRow);
        Concert CreateConcert(string name, DateTime startTime, string venueId);

        List<Seat> FindBestAvailableSeats(string concertId, int count);
        SeatHold HoldSeats(string concertId, Customer customer, List<string> seatIds, TimeSpan holdDuration);
    }
}
