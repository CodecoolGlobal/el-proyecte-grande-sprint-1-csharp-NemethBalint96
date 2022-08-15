using System.Runtime.InteropServices;
using ElProyecteGrande.Models;
using OurNonfictionBackend.Dal.Repositories;

namespace ElProyecteGrande.Dal;
public class BookingService : IBookingService
{
    private readonly IRepository<Booking> _bookingRepository;

    public BookingService(IRepository<Booking> repository)
    {
        _bookingRepository = repository;
    }

    public Task<List<Booking>> GetAll()
    {
        return _bookingRepository.GetAll();
    }

    public Task<Booking>? Get(long bookingId)
    {
        return _bookingRepository.Get(bookingId);
    }

    public async Task Add(Booking booking)
    {
        await _bookingRepository.Add(booking);
    }

  

    public async Task SetStatusCancelled(long bookingId)
    {
        await _bookingRepository.SetStatusCancelled(bookingId);
    }
    public async Task Update(Booking booking, long bookingId)
    {
        await  _bookingRepository.Update(booking,bookingId);
    }

   

    public async Task DeleteGuestFromBooking(long guestId)
    {
         await _bookingRepository.DeleteGuestsFromBooking(guestId);
    }

    

    public async Task<Guest>? GetGuest(long guestId)
    {
        return await _bookingRepository.GetGuest(guestId);
    }

    public async Task EditGuest(Guest newGuest)
    {
        await _bookingRepository.EditGuest(newGuest);
    }

    public async Task<IEnumerable<Guest>> GetAllNamedGuests()
    {
        
        return await _bookingRepository.GetAllNamedGuest();
    }

    public async Task AddNewGuestToBooking(long bookingId, Guest guest)
    {
        await _bookingRepository.AddNewGuestToBooking(bookingId, guest);
    }

    public Task<Booking> GetLatestBooking()
    {
        return _bookingRepository.GetLatestBooking();
    }

    public Task<Guest> GetLatestGuest()
    {
        return _bookingRepository.GetLatestGuest();
    }
}
