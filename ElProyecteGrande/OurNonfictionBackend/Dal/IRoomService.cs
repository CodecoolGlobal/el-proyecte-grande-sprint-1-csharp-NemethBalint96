using ElProyecteGrande.Models;

namespace ElProyecteGrande.Dal;
public interface IRoomService
{
    Task<List<Room>> GetAll();
    Task<Room>? Get(int roomId);
}
