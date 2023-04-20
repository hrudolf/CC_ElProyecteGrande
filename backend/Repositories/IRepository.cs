namespace backend.Repositories;

public interface IRepository<T>
{
    T Create(T item);
    IEnumerable<T> GetAll();
    T? GetById(int id);
    T Delete(int id);
    T Update(T updatedData);
}