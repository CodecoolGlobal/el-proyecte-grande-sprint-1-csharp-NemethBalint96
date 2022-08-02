using ElProyecteGrande.Models;

namespace OurNonfictionBackend.Dal;
public interface IBookingDetailsService
{
    IEnumerable<Room> FilterRoomsByBookingDate(int bookingId);
    void AddRoomToBooking(int roomId, int bookingId);
}
