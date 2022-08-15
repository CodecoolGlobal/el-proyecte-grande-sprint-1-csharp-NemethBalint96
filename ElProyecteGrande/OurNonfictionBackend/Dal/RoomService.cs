using ElProyecteGrande.Models;
using OurNonfictionBackend.Dal.Repositories;

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

    public Task<List<Room>> GetAll()
    {
        return _roomRepository.GetAll();
    }

    public async Task<Room>? Get(int roomId)
    {
        return await _roomRepository.Get(roomId);
    }
}
