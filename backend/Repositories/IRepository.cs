namespace backend.Repositories;

public interface IRepository<T>
{
    void AddNewItem();
    
    HashSet<T> GetAllItems();
    
    T GetItemById(int id);

    void UpdateItemById(int id);

    void DeleteItemById(int id);

}