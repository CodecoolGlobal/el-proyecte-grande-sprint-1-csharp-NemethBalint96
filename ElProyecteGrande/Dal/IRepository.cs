namespace ElProyecteGrande.Dal;
public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T? Get(int id);
    void Add(T item);
    bool Delete(int id);
    void Update(T item);
}
