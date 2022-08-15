using ElProyecteGrande.Models;

namespace ElProyecteGrande.Dal;
public interface IBookingService
{
    Task<List<Booking>> GetAll();
    Task<Booking>? Get(long bookingId);
    Task Add(Booking booking);
    Task Update(Booking booking,long bookingId);
    Task SetStatusCancelled(long bookingId);
    Task DeleteGuestFromBooking(long guestId);
    Task<Guest>? GetGuest(long guestId);
    Task EditGuest(Guest guest);
    Task<IEnumerable<Guest>> GetAllNamedGuests();
    Task AddNewGuestToBooking(long bookingId, Guest guest);
    Task<Booking> GetLatestBooking();
    Task<Guest> GetLatestGuest();
}
