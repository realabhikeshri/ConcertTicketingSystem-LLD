using ConcertTicketingSystem_LLD.Enums;
using ConcertTicketingSystem_LLD.Models;
using ConcertTicketingSystem_LLD.Services;

namespace ConcertTicketingSystem;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== Concert Ticketing System (C# LLD) ===");

        ConcertService concertService = ConcertService.Instance;
        var bookingService = new BookingService(concertService.BookingRepository);
        var paymentService = new PaymentService();

        // Setup venue and concert
        var venue = concertService.CreateVenue("Amsterdam Arena", "Amsterdam", rows: 3, seatsPerRow: 5);
        var concert = concertService.CreateConcert("Rock Night", DateTime.UtcNow.AddDays(7), venue.Id);

        Console.WriteLine($"Created concert '{concert.Name}' at {venue.Name}");

        // Customer
        var customer = new Customer(Guid.NewGuid().ToString(), "Alice", "alice@example.com");

        // Find best 3 seats
        var seats = concertService.FindBestAvailableSeats(concert.Id, 3);
        Console.WriteLine("Best available seats:");
        foreach (var s in seats)
        {
            Console.WriteLine($"Seat {s.Row}-{s.Number} (Price: {s.Price})");
        }

        // Hold seats
        var hold = concertService.HoldSeats(concert.Id, customer, seats.Select(s => s.Id).ToList(), TimeSpan.FromMinutes(5));
        Console.WriteLine($"Held {hold.SeatIds.Count} seats for customer {customer.Name}");

        // Confirm booking
        var booking = bookingService.ConfirmBooking(hold);
        Console.WriteLine($"Booking created: {booking.Id}, amount: {booking.TotalAmount}, status: {booking.Status}");

        // Process payment
        var payment = paymentService.ProcessPayment(booking);
        Console.WriteLine($"Payment status: {payment.Status}");

        Console.WriteLine("Demo finished.");
    }
}
