using ElProyecteGrande.Models;

namespace OurNonfictionBackend.Dal;
public interface IBookingDetailsService
{
    Task<List<Room>> FilterRoomsByBookingDate(long bookingId);
    Task AddRoomToBooking(long roomId, long bookingId);
}
