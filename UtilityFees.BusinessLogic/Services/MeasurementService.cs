using UtilityFees.BusinessLogic.Interfaces;
using UtilityFeesApp.BusinessLogic.ViewModels;
using UtilityFeesAppData.Entities;
using UtilityFeesAppData.Interfaces;
using UtilityFeesAppData.Repositories;

namespace UtilityFees.BusinessLogic.Services;

public class MeasurementService : IMeasurementService
{
    private readonly IRepository<User> _userRep;
    private readonly IRepository<FullMeasurement> _measRep;

    public MeasurementService(IRepository<User> userRep, IRepository<FullMeasurement> measRep)
    {
        _userRep = userRep;
        _measRep = measRep;
    }

    public MeasurementViewModel GetMeasurement(string userId, int month)
    {
        var user = _userRep.GetById(userId);
        var measurement = user.Measurements.FirstOrDefault(meas => meas.MeasurementMonth == month);
        var measModel = new MeasurementViewModel()
        {

        };
        return measModel;
    }

    public void AddMeasurement(string userId, MeasurementViewModel measurement)
    {
        var user = _userRep.GetById(userId);
        var meas = _measRep.GetAll()
            .FirstOrDefault(measur => measur.User == user && measur.MeasurementMonth == measurement.MeasurementMonth);
        if (meas == null)
        {
            meas = new FullMeasurement()
            {
                ColdWaterAmount = measurement.ColdWaterAmount,
                DailyElectricityAmount = measurement.DailyElectricityAmount,
                HotWaterAmount = measurement.HotWaterAmount,
                MeasurementMonth = measurement.MeasurementMonth,
                NightElectricityAmount = measurement.NightElectricityAmount,
                User = user
            };
            _measRep.Create(meas);
        }
        else
        {
            meas.ColdWaterAmount = measurement.ColdWaterAmount;
            meas.DailyElectricityAmount = measurement.DailyElectricityAmount;
            meas.HotWaterAmount = measurement.HotWaterAmount;
            meas.NightElectricityAmount = measurement.NightElectricityAmount;
            _measRep.SaveChanges();
        }
    }
}