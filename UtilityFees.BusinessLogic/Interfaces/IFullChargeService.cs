using UtilityFeesApp.BusinessLogic.ViewModels;

namespace UtilityFees.BusinessLogic.Interfaces;

public interface IFullChargeService
{
    FullChargeViewModel CalcCharge(MeasurementViewModel measurement, string userId);
}