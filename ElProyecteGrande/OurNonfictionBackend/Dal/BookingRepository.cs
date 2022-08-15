using ElProyecteGrande.Models;
using Microsoft.EntityFrameworkCore;
using OurNonfictionBackend.Dal.Repositories;
using OurNonfictionBackend.Models;

namespace ElProyecteGrande.Dal;
public class BookingRepository : IRepository<Booking>
{
    private NonfictionContext _context;
    public BookingRepository(NonfictionContext context)
    {
        _context = context;
    }


    public async Task<List<Booking>>GetAll()
    {
        return await _context.Bookings.Include(x=>x.Guests).Include(booking=>booking.Room).AsNoTracking().ToListAsync();
    }

    public async  Task<Booking>? Get(long bookingId)
    {
        return await _context.Bookings.Include(x=>x.Guests).Include(x=>x.Room).FirstAsync(booking => booking.Id == bookingId);
    }

    public async Task Add(Booking booking)
    {
        CreateGuests(booking.Adults, booking.Children, booking.Infants, booking.Guests);
        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(long bookingId)
    {
        var booking = await  Get(bookingId);
        if (booking != null)
            _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();

    }

    public async Task Update(Booking booking, long bookingId)
    {
        var oldBooking =  _context.Bookings.First(b=>b.Id==bookingId);
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

    public async Task DeleteGuestsFromBooking(long guestId)
    {
        var booking = _context.Bookings.First(booking => booking.Guests.Any(guest => guest.Id == guestId));
        var guest = _context.Guests.First(guest => guest.Id == guestId);
        DecreaseGuestNumber(booking,guest.Age);
        _context.Guests.Remove(guest);
        await _context.SaveChangesAsync();
    }

    public async Task<Guest> GetGuest(long guestId)
    {
        return _context.Guests.First(g=>g.Id==guestId);
    }

    public async Task EditGuest(Guest newGuest)
    {
        _context.Guests.Update(newGuest);
        await _context.SaveChangesAsync();
    }

    public async Task AddNewGuestToBooking(long bookingId, Guest guest)
    {
        var booking = _context.Bookings.First(b=>b.Id==bookingId);
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

    public async Task<IEnumerable<Guest>> GetAllNamedGuest()
    {
        return await _context.Guests.Where(guest=>guest.FullName!="Accompanying Guest").ToListAsync();
    }

    public async Task AddRoomToBooking(long roomId, long bookingId)
    {
        var booking = _context.Bookings.First(b => b.Id == bookingId);
        booking.Room = _context.Rooms.First(r => r.Id == roomId);
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
    }

    public Task<List<Room>> FilterRoomsByBookingDate(long bookingId)
    {
        throw new NotImplementedException();
    }

    public async Task<Booking> GetLatestBooking()
    {
        return  _context.Bookings.OrderByDescending(b => b.Id).First();
    }

    public async Task<Guest> GetLatestGuest()
    {
        return _context.Guests.OrderByDescending(g => g.Id).First();
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
}
