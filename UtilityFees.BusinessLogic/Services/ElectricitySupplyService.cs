using UtilityFees.BusinessLogic.Interfaces;

namespace UtilityFees.BusinessLogic.Services;

public class ElectricitySupplyService : IElectricitySupplyService
{
    private const double Standard = 164;
    private const double Rate = 4.28;
    private const double RateForNight = 2.31;
    private const double RateForDay = 4.9;
    
    public double CountCharge(int numOfResidents)
    {
        var volume = CountVolume(numOfResidents);
        var charge = volume * Rate;
        return charge;
    }

    public double CountVolume(int numOfResidents)
    {
        var volume = numOfResidents * Standard;
        return volume;
    }

    public double CountVolume(double indications, double pastIndications)
    {
        var volume = indications - pastIndications;
        return volume;
    }

    public double CountChargeForDays(double indications, double pastIndications)
    {
        var volume = CountVolume(indications, pastIndications);
        var charge = volume * RateForDay;
        return charge;
    }

    public double CountChargeForNights(double indications, double pastIndications)
    {
        var volume = CountVolume(indications, pastIndications);
        var charge = volume * RateForNight;
        return charge;
    }
}