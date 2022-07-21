using ElProyecteGrande.Models;

namespace ElProyecteGrande.Dao;

public class RoomDaoMemory
{
    private List<Room> _rooms;
    private static RoomDaoMemory _instance;

    private RoomDaoMemory()
    {
        _rooms = new List<Room>();
    }

    public static RoomDaoMemory GetInstance()
    {
        if (_instance == null)
        {
            _instance = new RoomDaoMemory();
        }
        return _instance;
    }

    public void Add(Room room)
    {
        _rooms.Add(room);
    }

    public IEnumerable<Room> GetAll()
    {
        return _rooms;
    }

    public IEnumerable<Room> GetAvailable(Booking booking)
    {
        var available = new List<Room>();
        foreach (Room room in _rooms)
        {
            if (room.Bookings.Count == 0)
            {
                available.Add(room);
                continue;
            }

            foreach (var reservation in room.Bookings)
            {
                if ((reservation.ArrivalDate.Date < booking.DepartureDate.Date ||
                     reservation.ArrivalDate.Date <= booking.ArrivalDate.Date) &&
                    (reservation.DepartureDate.Date > booking.ArrivalDate.Date ||
                     reservation.DepartureDate.Date >= booking.DepartureDate.Date))
                {
                    available.Remove(room);
                    continue;
                }
                available.Add(room);
            }
        }
        return available;
    }

    public Room Get(int id)
    {
        return _rooms.First(room => room.ID == id);
    }

    public void ChangeRoom(Booking booking)
    {
        var bookings = _rooms;
        var rooms = new List<Room>();
        foreach (var room in _rooms)
        {
            rooms.Add(room);
            var reservations = new List<Booking>();
            foreach (var b in room.Bookings)
            {
                if (b.ID != booking.ID)
                {
                    reservations.Add(b);
                }
            }
            room.Bookings = reservations;
        }
        _rooms = rooms;
    }
}
