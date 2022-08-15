using ElProyecteGrande.Models;

namespace OurNonfictionBackend.Dal.Repositories;
public interface IRepository<T>
{
    Task<List<T>> GetAll();
    Task<T>? Get(long bookingId);
    Task Add(T item);
    Task Delete(long bookingId);
    Task Update(T item,long bookingId);
    Task SetStatusCancelled(long bookingId);
    Task DeleteGuestsFromBooking(long guestId);
    Task<Guest> GetGuest(long guestId);
    Task EditGuest(Guest newGuest);
    Task AddNewGuestToBooking(long bookingId, Guest guest);
    Task<IEnumerable<Guest>> GetAllNamedGuest();
    Task AddRoomToBooking(long roomId, long bookingId);
    Task<List<Room>> FilterRoomsByBookingDate(long bookingId);
    Task<Booking> GetLatestBooking();
    Task<Guest> GetLatestGuest();
}
