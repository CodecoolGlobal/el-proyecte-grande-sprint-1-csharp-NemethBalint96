using ElProyecteGrande.Models;
using OurNonfictionBackend.Dal.Repositories;

namespace OurNonfictionBackend.Dal;
public class BookingDetailsService : IBookingDetailsService
{
    private readonly IRepository<Booking> _bookingRepository;
    private readonly IRepository<Room> _roomRepository;

    public BookingDetailsService(IRepository<Booking> bookingRepository, IRepository<Room> roomRepository)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
    }

    public async Task<List<Room>> FilterRoomsByBookingDate(long bookingId)
    {

         return await _roomRepository.FilterRoomsByBookingDate(bookingId);
    }


    public IEnumerable<Room> FilterRoomsByBookingDate(int bookingId)
    {
        throw new NotImplementedException();
    }

    public async Task AddRoomToBooking(long roomId, long bookingId)
    {

        await _bookingRepository.AddRoomToBooking(roomId, bookingId);
    }
}
