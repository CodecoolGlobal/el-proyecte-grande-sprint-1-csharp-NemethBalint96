using ElProyecteGrande.Models;

namespace ElProyecteGrande.Dal;
public class RoomRepository : IRepository<Room>
{
    private readonly List<Room> _rooms;

    public RoomRepository()
    {
        _rooms = new List<Room>();
        Initialize();
    }

    private void Initialize()
    {
        for (var i = 1; i < 6; i++)
        {
            var room = new Room
            {
                DoorNumber = i,
                Floor = 1,
                RoomType = RoomType.Standard,
                Price = 80
            };
            _rooms.Add(room);
        }
        for (var i = 1; i < 6; i++)
        {
            var room = new Room
            {
                DoorNumber = i,
                Floor = 2,
                RoomType = RoomType.Superior,
                Price = 100
            };
            _rooms.Add(room);
        }
        for (var i = 1; i < 6; i++)
        {
            var room = new Room
            {
                DoorNumber = i,
                Floor = 3,
                RoomType = RoomType.Apartman,
                Price = 150
            };
            _rooms.Add(room);
        }
    }

    public IEnumerable<Room> GetAll()
    {
        return _rooms;
    }

    public Room? Get(int bookingId)
    {
        return _rooms.FirstOrDefault(room => room.Id == bookingId);
    }

    public void Add(Room room)
    {
        _rooms.Add(room);
    }

    public bool Delete(int bookingId)
    {
        var room = Get(bookingId);
        if (room != null)
            return _rooms.Remove(room);
        return false;
    }

    public void Update(Room room)
    {
        var isDeleted = Delete(room.Id);
        if (isDeleted)
            Add(room);
    }
}
