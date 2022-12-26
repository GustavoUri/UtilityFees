using UtilityFees.BusinessLogic.Interfaces;

namespace UtilityFees.BusinessLogic.Services;

public class HeatEnergyService : IHESupplyService
{
    private const double Standard = 0.05349;
    private const double Rate = 998.69;
    private readonly IHCSupplyService _heatCarrService;
    public HeatEnergyService(IHCSupplyService heatCarrService)
    {
        _heatCarrService = heatCarrService;
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

    public double CalcVolume(int numOfResidents)
    {
        var heatCarrVolume = _heatCarrService.CalcVolume(numOfResidents);
        var volume = heatCarrVolume * Standard;
        return volume;
    }

    public double CalcVolume(double indications, double pastIndications)
    {
        var heatCarrVolume = _heatCarrService.CalcVolume(indications, pastIndications);
        var volume = heatCarrVolume * Standard;
        return volume;
    }
}