using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;

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

    public IEnumerable<Room> FilterRoomsByBookingDate(int bookingId)
    {
        var booking = _bookingRepository.Get(bookingId);
        var notCancelledBookings = _bookingRepository.GetAll().Where(b => b.Status != Status.Cancelled);
        var available = _roomRepository.GetAll().ToList();
        foreach (var room in _roomRepository.GetAll())
        {
            foreach (var reservation in notCancelledBookings)
            {
                if (reservation.Room?.Id == room.Id &&
                    (reservation.ArrivalDate.Date < booking.DepartureDate.Date ||
                     reservation.ArrivalDate.Date <= booking.ArrivalDate.Date) &&
                    (reservation.DepartureDate.Date > booking.ArrivalDate.Date ||
                     reservation.DepartureDate.Date >= booking.DepartureDate.Date))
                {
                    available.Remove(room);
                }
            }
        }
        return available;
    }

    public bool AddRoomToBooking(int roomId, int bookingId)
    {
        var booking = _bookingRepository.Get(bookingId);
        var room = _roomRepository.Get(roomId);
        if ((booking is null) || (room is null))
            return false;

        if (booking.Room == null)
        {
            booking.Room = new Room();
        }
        booking.Room.Id = room.Id;
        booking.Room.Floor = room.Floor;
        booking.Room.DoorNumber = room.DoorNumber;
        booking.Room.RoomType = room.RoomType;
        booking.Room.Price = room.Price;
        booking.Room.Comment = room.Comment;
        return true;
    }
}
