namespace backend.Repositories;

public interface IRepository<T>
{
    void AddNewItem();
    
    HashSet<T> GetAllItems();
    
    T GetItemById(int id);

    T UpdateItemById(T item);

    void DeleteItemById(int id);

}