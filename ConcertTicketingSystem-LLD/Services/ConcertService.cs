using ConcertTicketingSystem_LLD.Exceptions;
using ConcertTicketingSystem_LLD.Models;
using ConcertTicketingSystem_LLD.Repositories;
using ConcertTicketingSystem_LLD.Strategies.Allocation;
using ConcertTicketingSystem_LLD.Strategies.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Services
{
    public sealed class ConcertService : IConcertService
    {
        private static readonly object _sync = new();
        private static ConcertService? _instance;
        public static ConcertService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_sync)
                    {
                        _instance ??= new ConcertService();
                    }
                }
                return _instance;
            }
        }

        private readonly IConcertRepository _concertRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IPricingStrategy _pricingStrategy;

        // HoldId -> SeatHold
        private readonly Dictionary<string, SeatHold> _holds = new();
        private readonly object _holdsLock = new();

        public IBookingRepository BookingRepository => _bookingRepository;

        private ConcertService()
        {
            _concertRepository = new InMemoryConcertRepository();
            _bookingRepository = new InMemoryBookingRepository();
            _pricingStrategy = new FlatPricingStrategy();
        }

        public Venue CreateVenue(string name, string location, int rows, int seatsPerRow)
        {
            var venue = new Venue(Guid.NewGuid().ToString(), name, location);

            for (int r = 1; r <= rows; r++)
            {
                for (int s = 1; s <= seatsPerRow; s++)
                {
                    decimal price = 50 + (rows - r) * 10; // closer rows more expensive
                    var seat = new Seat(Guid.NewGuid().ToString(), r, s, price);
                    venue.AddSeat(seat);
                }
            }

            _concertRepository.AddVenue(venue);
            return venue;
        }

        public Concert CreateConcert(string name, DateTime startTime, string venueId)
        {
            var venue = _concertRepository.GetVenueById(venueId)
                ?? throw new ArgumentException("Invalid venue id");

            var concert = new Concert(Guid.NewGuid().ToString(), name, startTime, venue);
            _concertRepository.AddConcert(concert);
            return concert;
        }

        public List<Seat> FindBestAvailableSeats(string concertId, int count)
        {
            var concert = GetConcert(concertId);
            var available = concert.GetAvailableSeats();
            var strategy = new BestAvailableSeatStrategy();
            return strategy.AllocateSeats(available, count);
        }

        public SeatHold HoldSeats(string concertId, Customer customer, List<string> seatIds, TimeSpan holdDuration)
        {
            var concert = GetConcert(concertId);

            var seats = seatIds
                .Select(id => concert.GetSeatById(id) ?? throw new SeatNotAvailableException("Seat not found"))
                .ToList();

            // Try to hold each seat atomically at seat level
            foreach (var seat in seats)
            {
                if (!seat.TryHold())
                {
                    // rollback already held seats
                    foreach (var s in seats.Where(s => s.Status == Enums.SeatStatus.Held))
                    {
                        s.Release();
                    }
                    throw new SeatNotAvailableException("One or more seats are no longer available");
                }
            }

            var holdId = Guid.NewGuid().ToString();
            var expiry = DateTime.UtcNow.Add(holdDuration);
            var hold = new SeatHold(holdId, concertId, customer.Id, seatIds, expiry);

            lock (_holdsLock)
            {
                _holds[holdId] = hold;
            }

            return hold;
        }

        public SeatHold GetHoldById(string holdId)
        {
            lock (_holdsLock)
            {
                _holds.TryGetValue(holdId, out var hold);
                return hold ?? throw new ArgumentException("Invalid hold id");
            }
        }

        public (Concert concert, List<Seat> seats, decimal totalPrice) ConvertHoldToSeatsAndPrice(SeatHold hold)
        {
            var concert = GetConcert(hold.ConcertId);

            var seats = hold.SeatIds
                .Select(id => concert.GetSeatById(id) ?? throw new SeatNotAvailableException("Seat not found"))
                .ToList();

            decimal totalPrice = _pricingStrategy.CalculateTotalPrice(seats);
            return (concert, seats, totalPrice);
        }

        private Concert GetConcert(string concertId)
        {
            return _concertRepository.GetConcertById(concertId)
                ?? throw new ArgumentException("Invalid concert id");
        }
    }
}
