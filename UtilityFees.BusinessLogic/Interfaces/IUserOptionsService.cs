using UtilityFees.BusinessLogic.ViewModels;

namespace UtilityFees.BusinessLogic.Interfaces;

public interface IUserOptionsService
{
    void SetNumberOfResidents(string userId, int numberOfResidents);
    void SetColdWaterSupplyDevice(string userId, bool hasSupplyDevice);
    void SetHotWaterSupplyDevice(string userId, bool hasSupplyDevice);
    void SetPowerSupplyDevice(string userId, bool hasSupplyDevice);

    void SetUserOptions(UserOptionsViewModel options, string userId);
    UserOptionsViewModel GetUserOptions(string userId);

}