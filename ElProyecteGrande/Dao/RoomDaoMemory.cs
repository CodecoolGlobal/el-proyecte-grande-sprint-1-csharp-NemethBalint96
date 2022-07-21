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
}
