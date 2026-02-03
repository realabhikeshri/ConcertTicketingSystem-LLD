# 🎫 Concert Ticketing System – C# LLD

A **production-style**, **interview-focused** Concert Ticketing System implemented in C#
It demonstrates:

- Clean **OO modeling** of concerts, venues, seats, bookings, and payments.
- **Seat-level locking** to avoid double-booking under concurrency.
- **Strategy pattern** for pricing & seat allocation.
- **Repository pattern** with in-memory stores (easy to swap to DB later).
- A **simple console demo** to walk your interviewer through the flow.

---

## System Requirements

The system supports:

- Creating venues and concerts.
- Representing seat maps (rows, seat numbers, categories, prices).
- Viewing available seats for a concert.
- Holding seats for a short time while payment is processed.
- Confirming bookings and marking seats as **BOOKED**.
- Cancelling bookings and releasing seats back to **AVAILABLE**.

---

## Design Highlights

### Core Entities

- **User / Customer** – person booking tickets.
- **Venue** – name, location, list of seats.
- **Concert** – artist, date/time, venue, seat inventory.
- **Seat** – row, number, category, price, status.
- **SeatHold** – temporary hold for seats with expiry (simulated).
- **Booking** – confirmed ticket booking with status.
- **Payment** – simple payment object (mocked success).

### Key Enums

- `SeatStatus`: `Available`, `Held`, `Booked`
- `BookingStatus`: `Pending`, `Confirmed`, `Cancelled`
- `PaymentStatus`: `Pending`, `Successful`, `Failed`

### Patterns Used

- **Strategy**
  - `IPricingStrategy` with `FlatPricingStrategy`, `DynamicPricingStrategy`
  - `ISeatAllocationStrategy` with `BestAvailableSeatStrategy`, `ExactSeatSelectionStrategy`
- **Repository**
  - `IConcertRepository`, `IBookingRepository`
- **Singleton**
  - `ConcertService` accessed via `Instance` for quick demo wiring.
- **Separation of Concerns**
  - `ConcertService` = read/seat allocation/holding.
  - `BookingService` = booking lifecycle.
  - `PaymentService` = payment simulation.

### Concurrency

- Seats are stored per concert with **per-seat locking** using `lock` and `ConcurrentDictionary`.
- Seat status transitions: `Available → Held → Booked`, and back to `Available` on cancellation/expiry.
- This makes it easy to talk about concurrency, double-booking, and potential extension to distributed locks.

---

## Project Structure

```text
ConcertTicketingSystem/
├── Enums/          # SeatStatus, BookingStatus, PaymentStatus
├── Models/         # Concert, Venue, Seat, SeatHold, Booking, Payment
├── Strategies/     # Pricing + Seat allocation
├── Repositories/   # In-memory repositories
├── Services/       # ConcertService, BookingService, PaymentService
└── Exceptions/     # SeatNotAvailableException, BookingNotFoundException

## Author
-Abhishek Keshri
