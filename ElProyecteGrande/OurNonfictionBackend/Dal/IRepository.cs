namespace ElProyecteGrande.Dal;
public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T? Get(int bookingId);
    void Add(T item);
    bool Delete(int bookingId);
    void Update(T item);
}
