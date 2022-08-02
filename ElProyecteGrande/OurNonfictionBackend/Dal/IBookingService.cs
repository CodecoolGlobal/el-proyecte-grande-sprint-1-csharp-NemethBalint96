using ElProyecteGrande.Models;

namespace ElProyecteGrande.Dal;
public interface IBookingService
{
    IEnumerable<Booking> GetAll();
    Booking? Get(int bookingId);
    void Add(Booking booking);
    void Update(Booking booking);
    void SetStatusCancelled(int bookingId);
    void DeleteGuestFromBooking(int guestId);
    Guest? GetGuest(int guestId);
    void EditGuest(Guest guest);
    IEnumerable<Guest> GetAllNamedGuests();
}
