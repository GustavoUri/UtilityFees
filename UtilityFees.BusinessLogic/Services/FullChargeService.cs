using UtilityFees.BusinessLogic.Interfaces;
using UtilityFeesApp.BusinessLogic.ViewModels;
using UtilityFeesAppData.Entities;
using UtilityFeesAppData.Interfaces;

namespace UtilityFees.BusinessLogic.Services;

public class FullChargeService : IFullChargeService
{
    private readonly IRepository<User> _userRep;
    private readonly IRepository<FullMeasurement> _measRep;
    private readonly ICWSupplyService _coldWaterService;
    private readonly IHCSupplyService _heatCarrierService;
    private readonly IHESupplyService _heatEnergyService;
    private readonly IMeasurementService _measService;
    private readonly IElectricitySupplyService _electrSupplyService;

    public FullChargeService(ICWSupplyService coldWaterService,
        IHCSupplyService heatCarrierService, IHESupplyService heatEnergyService,
        IMeasurementService measService, IElectricitySupplyService electrSupplyService,
        IRepository<FullMeasurement> measRep, IRepository<User> userRep)
    {
        _measService = measService;
        _coldWaterService = coldWaterService;
        _electrSupplyService = electrSupplyService;
        _heatCarrierService = heatCarrierService;
        _heatEnergyService = heatEnergyService;
        _measRep = measRep;
        _userRep = userRep;
    }

    private FullMeasurement GetPastMeas(string userId, int month)
    {
        var pastMeas = _measRep.GetAll()
                           .FirstOrDefault(
                               meas => meas.UserId == userId &&
                                       meas.MeasurementMonth == (month - 1))
                       ?? new FullMeasurement()
                       {
                           ColdWaterAmount = 0, DailyElectricityAmount = 0, HotWaterAmount = 0,
                           NightElectricityAmount = 0
                       };
        return pastMeas;
    }

    public FullChargeViewModel CalcCharge(MeasurementViewModel measurement, string userId)
    {
        
        var user = _userRep.GetById(userId);
        var pastMeas = GetPastMeas(userId, measurement.MeasurementMonth);
        var fullCharge = new FullChargeViewModel
        {
            FullPayment = 0
        };
        if (measurement.DailyElectricityAmount != 0 && measurement.NightElectricityAmount != 0)
        {
            fullCharge.DailyElectricityCharge = _electrSupplyService.CountChargeForDays(
                measurement.DailyElectricityAmount,
                pastMeas.DailyElectricityAmount);

            fullCharge.NightElectricityCharge = _electrSupplyService.CountChargeForNights(
                measurement.NightElectricityAmount,
                pastMeas.NightElectricityAmount);
            fullCharge.FullPayment += fullCharge.DailyElectricityCharge;
            fullCharge.FullPayment += fullCharge.NightElectricityCharge;
        }
        else
        {
            fullCharge.ElectricityCharge = _electrSupplyService.CountCharge(user.NumberOfResidents);
            fullCharge.FullPayment += fullCharge.ElectricityCharge;
        }

        if (measurement.HotWaterAmount != 0)
        {
            fullCharge.HeatCarrierCharge =
                _heatCarrierService.CalcCharge(measurement.HotWaterAmount, pastMeas.HotWaterAmount);
            fullCharge.HeatEnergyCharge =
                _heatEnergyService.CalcCharge(measurement.HotWaterAmount, pastMeas.HotWaterAmount);
        }
        else
        {
            fullCharge.HeatCarrierCharge = _heatCarrierService.CalcCharge(user.NumberOfResidents);
            fullCharge.HeatEnergyCharge = _heatEnergyService.CalcCharge(user.NumberOfResidents);
        }
        fullCharge.FullPayment += fullCharge.HeatCarrierCharge;
        fullCharge.FullPayment += fullCharge.HeatEnergyCharge;
        if (measurement.ColdWaterAmount != 0)
        {
            fullCharge.ColdWaterCharge =
                _coldWaterService.CalcCharge(measurement.ColdWaterAmount, pastMeas.ColdWaterAmount);
        }
        else
        {
            fullCharge.ColdWaterCharge = _coldWaterService.CalcCharge(user.NumberOfResidents);
        }
        fullCharge.FullPayment += fullCharge.ColdWaterCharge;

        return fullCharge;
    }
}