using ElProyecteGrande.Models;

namespace ElProyecteGrande.Dal;
public interface IBookingService
{
    IEnumerable<Booking> GetAll();
    Booking? Get(int roomId);
    void Add(Booking room);
    void Update(Booking room);
    void SetStatusCancelled(int id);
    void DeleteGuestFromBooking(int bookingId, int guestId);
    Guest? GetGuest(int id);
    Booking EditGuestReturnBooking(Guest guest);
    Booking AddRoomToBooking(int id, Room room);
}
