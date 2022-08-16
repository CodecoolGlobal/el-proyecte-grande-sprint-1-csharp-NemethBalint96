using ElProyecteGrande.Models;

namespace OurNonfictionBackend.Dal;
public interface IGuestService
{
    Task DeleteGuestFromBooking(long guestId);
    Task<Guest>? GetGuest(long guestId);
    Task EditGuest(Guest guest);
    Task AddNewGuestToBooking(long bookingId, Guest guest);
    Task<Guest> GetLatestGuest();
    Task<IEnumerable<Guest>> GetAllNamedGuests();
}
