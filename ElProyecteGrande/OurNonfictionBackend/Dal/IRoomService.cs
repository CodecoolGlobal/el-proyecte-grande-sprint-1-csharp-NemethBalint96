using ElProyecteGrande.Models;

namespace ElProyecteGrande.Dal;
public interface IRoomService
{
    Task<List<Room>> GetAll();
    Task<Room>? Get(long roomId);
    Task<List<Room>> FilterRoomsByBookingDate(long bookingId);
    Task AddRoomToBooking(long roomId, long bookingId);
}
