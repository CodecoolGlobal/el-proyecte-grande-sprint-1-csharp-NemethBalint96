using System.Runtime.InteropServices;
using ElProyecteGrande.Models;
using Microsoft.EntityFrameworkCore;
using OurNonfictionBackend.Models;

namespace ElProyecteGrande.Dal;
public class BookingService : IBookingService
{
    private readonly NonfictionContext _context;

    public BookingService(NonfictionContext context)
    {
        _context = context;
    }

    public async Task<List<Booking>> GetAll()
    {
        return await _context.Bookings.Include(x => x.Guests).Include(booking => booking.Room).AsNoTracking().ToListAsync();
    }

    public async Task<Booking>? Get(long bookingId)
    {
        return await _context.Bookings.Include(x => x.Guests).Include(x => x.Room).FirstAsync(booking => booking.Id == bookingId);
    }

    public async Task Add(Booking booking)
    {
        CreateGuests(booking.Adults, booking.Children, booking.Infants, booking.Guests);
        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Booking booking, long bookingId)
    {
        var oldBooking = _context.Bookings.First(b => b.Id == bookingId);
        _context.ChangeTracker.Clear();
        booking.Room = oldBooking.Room;
        booking.Guests.AddRange(oldBooking.Guests);
        CreatePlusGuests(oldBooking, booking);
        booking.ModificationDate = DateTime.Now;
        oldBooking = booking;
        oldBooking.Id = bookingId;
        _context.Bookings.Update(oldBooking);
        await _context.SaveChangesAsync();
    }

    private void CreatePlusGuests(Booking booking, Booking? editableBooking)
    {
        var adultsNumber = editableBooking.Adults - booking.Adults;
        var childrenNumber = editableBooking.Children - booking.Children;
        var infantsNumber = editableBooking.Infants - booking.Infants;
        CreateGuests(adultsNumber, childrenNumber,
            infantsNumber, editableBooking.Guests);
    }

    private void CreateGuests(int adults, int children, int infants, ICollection<Guest> guests)
    {
        for (var i = 0; i < adults; i++)
        {
            var adult = new Guest(Age.Adult);
            guests.Add(adult);
        }
        for (var i = 0; i < children; i++)
        {
            var child = new Guest(Age.Child);
            guests.Add(child);
        }
        for (var i = 0; i < infants; i++)
        {
            var infant = new Guest(Age.Infant);
            guests.Add(infant);
        }
    }

    public async Task SetStatusCancelled(long bookingId)
    {
        var booking = await Get(bookingId);
        booking.Status = Status.Cancelled;
        _context.Update(booking);
        await _context.SaveChangesAsync();
    }

    public async Task<Booking> GetLatestBooking()
    {
        return await _context.Bookings.OrderByDescending(b => b.Id).FirstAsync();
    }
}
