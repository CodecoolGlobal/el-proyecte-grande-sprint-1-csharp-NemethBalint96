using ElProyecteGrande.Models;

namespace ElProyecteGrande.Dal;
public interface IRoomService
{
    IEnumerable<Room> GetAll();
    Room? Get(int roomId);
}
