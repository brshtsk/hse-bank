namespace HseBank.Infrastructure.Data;

public interface IRepository<T>
{
    void Add(T entity);
    T Get(int id);
    void Update(T entity);
    void Delete(int id);
    IEnumerable<T> GetAll();
}