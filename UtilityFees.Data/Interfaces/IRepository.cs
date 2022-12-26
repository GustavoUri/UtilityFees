namespace UtilityFees.Data.Interfaces;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(string id);
    void Create(T item);
    void Delete(string id);
    void SaveChanges();
}