using ElProyecteGrande.Models;
using Microsoft.EntityFrameworkCore;
using OurNonfictionBackend.Models;

namespace ElProyecteGrande.Dal;
public class RoomService : IRoomService
{
    private readonly NonfictionContext _context;

    public RoomService(NonfictionContext context)
    {
        _context = context;
    }

    public async Task<List<Room>> GetAll()
    {
        return await _context.Rooms.ToListAsync();
    }

    public async Task<Room>? Get(long roomId)
    {
        return await _context.Rooms.FirstAsync(room => room.Id == roomId);
    }

    public async Task AddRoomToBooking(long roomId, long bookingId)
    {
        var booking = _context.Bookings.First(b => b.Id == bookingId);
        booking.Room = _context.Rooms.First(r => r.Id == roomId);
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Room>> FilterRoomsByBookingDate(long bookingId)
    {
        var booking = await _context.Bookings.FirstAsync(b => b.Id == bookingId);
        var notCancelledBookings = _context.Bookings.Include(b => b.Room).Where(x => x.Status != Status.Cancelled);
        var available = new List<Room>(await _context.Rooms.AsNoTracking().ToListAsync());
        foreach (var room in await _context.Rooms.AsNoTracking().ToListAsync())
        {
            foreach (var reservation in notCancelledBookings)
            {
                if (reservation.Room?.Id == room.Id &&
                    (reservation.ArrivalDate.Date < booking.DepartureDate.Date ||
                     reservation.ArrivalDate.Date <= booking.ArrivalDate.Date) &&
                    (reservation.DepartureDate.Date > booking.ArrivalDate.Date ||
                     reservation.DepartureDate.Date >= booking.DepartureDate.Date))
                {
                    available = available.Where(r => r.Id != reservation.Room?.Id).ToList();
                }
            }
        }
        return available;
    }
}
