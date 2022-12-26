using UtilityFees.BusinessLogic.Interfaces;

namespace UtilityFees.BusinessLogic.Services;

public class ColdWaterSupplyService : ICWSupplyService
{
    private const double Standard = 4.85;
    private const double Rate = 35.78;

    public double CalcVolume(double indications, double pastIndications)
    {
        var result = indications - pastIndications;
        return result;
    }

    public double CalcVolume(int residents)
    {
        var result = residents * Standard;
        return result;
    }

    public double CalcCharge(double indications, double pastIndications)
    {
        var volume = CalcVolume(indications, pastIndications);
        var charge = volume * Rate;
        return charge;
    }

    public double CalcCharge(int numOfResidents)
    {
        var volume = CalcVolume(numOfResidents);
        var charge = volume * Rate;
        return charge;
    }
}