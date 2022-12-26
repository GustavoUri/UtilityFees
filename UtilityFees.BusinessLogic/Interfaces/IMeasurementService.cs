using UtilityFees.BusinessLogic.ViewModels;

namespace UtilityFees.BusinessLogic.Interfaces;

public interface IMeasurementService
{
    void AddMeasurement(string userId, MeasurementViewModel measurement);
}