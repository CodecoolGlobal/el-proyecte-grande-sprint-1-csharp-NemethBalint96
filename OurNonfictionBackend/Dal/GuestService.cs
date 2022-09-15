using ElProyecteGrande.Models;
using Microsoft.EntityFrameworkCore;
using OurNonfictionBackend.Models;

namespace OurNonfictionBackend.Dal;
public class GuestService : IGuestService
{
    private readonly NonfictionContext _context;

    public GuestService(NonfictionContext context)
    {
        _context = context;
    }

    public async Task DeleteGuestFromBooking(long guestId)
    {
        var booking = _context.Bookings.First(booking => booking.Guests.Any(guest => guest.Id == guestId));
        var guest = _context.Guests.First(guest => guest.Id == guestId);
        DecreaseGuestNumber(booking, guest.Age);
        _context.Guests.Remove(guest);
        await _context.SaveChangesAsync();
    }

    private void DecreaseGuestNumber(Booking booking, Age age)
    {
        switch (age)
        {
            case Age.Adult:
                booking.Adults--;
                break;
            case Age.Child:
                booking.Children--;
                break;
            case Age.Infant:
                booking.Infants--;
                break;
        }
    }

    public async Task<Guest> GetGuest(long guestId)
    {
        return await _context.Guests.FirstAsync(g => g.Id == guestId);
    }

    public async Task EditGuest(Guest newGuest)
    {
        _context.Guests.Update(newGuest);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        var booking = await _context.Bookings.Include(b => b.Guests).FirstAsync(b => b.Guests.Any(guest => guest.Id == newGuest.Id));
        booking.Adults = booking.Guests.Count(g => g.Age == Age.Adult);
        booking.Children = booking.Guests.Count(g => g.Age == Age.Child);
        booking.Infants = booking.Guests.Count(g => g.Age == Age.Infant);

        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
    }

    public async Task AddNewGuestToBooking(long bookingId, Guest guest)
    {
        var booking = _context.Bookings.Include(booking => booking.Guests).First(b => b.Id == bookingId);
        switch (guest.Age)
        {
            case Age.Adult:
                booking.Adults++;
                break;
            case Age.Child:
                booking.Children++;
                break;
            case Age.Infant:
                booking.Infants++;
                break;
        }
        booking.Guests.Add(guest);
        await _context.SaveChangesAsync();
    }

    public async Task<Guest> GetLatestGuest()
    {
        return await _context.Guests.OrderByDescending(g => g.Id).FirstAsync();
    }

    public async Task<IEnumerable<Guest>> GetAllNamedGuests()
    {
        return await _context.Guests.Where(guest => guest.FullName != "Accompanying Guest").ToListAsync();
    }

}
