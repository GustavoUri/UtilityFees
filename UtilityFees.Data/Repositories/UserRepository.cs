using UtilityFees.Data.AppEFContext;
using UtilityFees.Data.Entities;
using UtilityFees.Data.Interfaces;

namespace UtilityFees.Data.Repositories;

public class UserRepository : IRepository<User>
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }
    
    public IEnumerable<User> GetAll()
    {
        return _db.Users;
    }

    public User GetById(string id)
    {
        return GetAll().FirstOrDefault(user => user.Id == id);
    }
    

    public void Create(User item)
    {
        _db.Users.Add(item);
        SaveChanges();
    }

    public void Delete(string id)
    {
        var user = GetById(id);
        _db.Users.Remove(user);
        SaveChanges();
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
    
}