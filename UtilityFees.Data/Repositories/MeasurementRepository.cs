using UtilityFees.Data.AppEFContext;
using UtilityFeesAppData.Entities;
using UtilityFeesAppData.Interfaces;

namespace UtilityFeesAppData.Repositories;

public class MeasurementRepository : IRepository<FullMeasurement>
{
    private readonly AppDbContext _db;

    public MeasurementRepository(AppDbContext db)
    {
        _db = db;
    }
    public IEnumerable<FullMeasurement> GetAll()
    {
        return _db.Measurements;
    }

    public FullMeasurement GetById(string id)
    {
        return _db.Measurements.Find(id);
    }

    public void Create(FullMeasurement item)
    {
        _db.Measurements.Add(item);
        SaveChanges();
    }

    public void Delete(string id)
    {
        var item = GetById(id);
        _db.Measurements.Remove(item);
        SaveChanges();
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}