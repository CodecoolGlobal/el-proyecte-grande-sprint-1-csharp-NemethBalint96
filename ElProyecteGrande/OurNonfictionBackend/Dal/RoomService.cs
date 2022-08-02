using ElProyecteGrande.Models;

namespace ElProyecteGrande.Dal;
public class RoomService : IRoomService
{
    private readonly IRepository<Room> _roomRepository;

    public RoomService(IRepository<Room> roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public void Add(Room room)
    {
        _roomRepository.Add(room);
    }

    public IEnumerable<Room> GetAll()
    {
        return _roomRepository.GetAll();
    }

    public Room? Get(int roomId)
    {
        return _roomRepository.Get(roomId);
    }
}
