using ElProyecteGrande.Models;
using Microsoft.EntityFrameworkCore;
using OurNonfictionBackend.Dal.Repositories;
using OurNonfictionBackend.Models;

namespace ElProyecteGrande.Dal;
public class RoomRepository : IRepository<Room>
{
    private NonfictionContext _context;

    public RoomRepository(NonfictionContext context)
    {
        _context = context;
    }


    public async Task<List<Room>> GetAll()
    {
        return await  _context.Rooms.ToListAsync();
    }

    public async Task<Room>? Get(long roomId)
    {
        return _context.Rooms.First(room => room.Id == roomId);
    }

    public async Task Add(Room room)
    {
       await  _context.Rooms.AddAsync(room);
       await _context.SaveChangesAsync();
    }

    public async Task Delete(long roomId)
    {
        var room = _context.Rooms.First(r=>r.Id==roomId);
        if (room != null)
            _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
    }

    public Task Update(Room item, long bookingId)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Room room)
    {
        var isDeleted = Delete(room.Id);
        await Add(room);
    }

    public Task SetStatusCancelled(long bookingId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteGuestsFromBooking(long guestId)
    {
        throw new NotImplementedException();
    }

    public Task<Guest> GetGuest(long guestId)
    {
        throw new NotImplementedException();
    }

    public Task EditGuest(Guest newGuest)
    {
        throw new NotImplementedException();
    }

    public Task AddNewGuestToBooking(long bookingId, Guest guest)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Guest>> GetAllNamedGuest()
    {
        throw new NotImplementedException();
    }

    public Task AddRoomToBooking(long roomId, long bookingId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Room>> FilterRoomsByBookingDate(long bookingId)
    {
        var booking = _context.Bookings.First(b=>b.Id==bookingId);
        var notCancelledBookings = _context.Bookings.Where(x => x.Status != Status.Cancelled);
        var available = _context.Rooms;
        foreach (var room in available)
        {
            foreach (var reservation in notCancelledBookings)
            {
                if (reservation.Room?.Id == room.Id &&
                    (reservation.ArrivalDate.Date < booking.DepartureDate.Date ||
                     reservation.ArrivalDate.Date <= booking.ArrivalDate.Date) &&
                    (reservation.DepartureDate.Date > booking.ArrivalDate.Date ||
                     reservation.DepartureDate.Date >= booking.DepartureDate.Date))
                {
                    available.Remove(room);
                }
            }
        }
        return available.ToListAsync();
    }

    public Task<Booking> GetLatestBooking()
    {
        throw new NotImplementedException();
    }

    public Task<Guest> GetLatestGuest()
    {
        throw new NotImplementedException();
    }
}
