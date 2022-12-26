using UtilityFees.BusinessLogic.Interfaces;
using UtilityFees.BusinessLogic.ViewModels;
using UtilityFees.Data.Entities;
using UtilityFees.Data.Interfaces;

namespace UtilityFees.BusinessLogic.Services;

public class UserOptionsService : IUserOptionsService
{
    private readonly IRepository<User> _userRep;

    public UserOptionsService(IRepository<User> userRep)
    {
        _userRep = userRep;
    }


    public void SetNumberOfResidents(string userId, int numberOfResidents)
    {
        var user = _userRep.GetById(userId);
        user.NumberOfResidents = numberOfResidents;
        _userRep.SaveChanges();
    }

    public void SetColdWaterSupplyDevice(string userId, bool hasSupplyDevice)
    {
        var user = _userRep.GetById(userId);
        user.HasCWSDevice = hasSupplyDevice;
        _userRep.SaveChanges();
    }

    public void SetHotWaterSupplyDevice(string userId, bool hasSupplyDevice)
    {
        var user = _userRep.GetById(userId);
        user.HasHWSDevice = hasSupplyDevice;
        _userRep.SaveChanges();
    }

    public void SetPowerSupplyDevice(string userId, bool hasSupplyDevice)
    {
        var user = _userRep.GetById(userId);
        user.HasPSDevice = hasSupplyDevice;
        _userRep.SaveChanges();
    }

    public void SetUserOptions(UserOptionsViewModel options, string userId)
    {
        SetNumberOfResidents(userId, options.NumberOfResidents);
        SetPowerSupplyDevice(userId, options.HasPSDevice);
        SetColdWaterSupplyDevice(userId, options.HasCWSDevice);
        SetHotWaterSupplyDevice(userId, options.HasHWSDevice);
    }

    public UserOptionsViewModel GetUserOptions(string userId)
    {
        var user = _userRep.GetById(userId);
        var userOptions = new UserOptionsViewModel()
        {
            HasPSDevice = user.HasPSDevice,
            HasCWSDevice = user.HasCWSDevice,
            HasHWSDevice = user.HasHWSDevice,
            NumberOfResidents = user.NumberOfResidents
        };
        return userOptions;
    }
}