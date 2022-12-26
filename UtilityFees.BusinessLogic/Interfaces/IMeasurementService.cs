using UtilityFeesApp.BusinessLogic.ViewModels;
using UtilityFeesAppData.Entities;

namespace UtilityFees.BusinessLogic.Interfaces;

public interface IMeasurementService
{
    MeasurementViewModel GetMeasurement(string userId, int month);
    void AddMeasurement(string userId, MeasurementViewModel measurement);
}