using UtilityFees.BusinessLogic.Interfaces;
using UtilityFees.BusinessLogic.ViewModels;
using UtilityFees.Data.Entities;
using UtilityFees.Data.Interfaces;

namespace UtilityFees.BusinessLogic.Services;

public class FullChargeService : IFullChargeService
{
    private readonly IRepository<User> _userRep;
    private readonly IRepository<FullMeasurement> _measRep;
    private readonly ICWSupplyService _coldWaterService;
    private readonly IHCSupplyService _heatCarrierService;
    private readonly IHESupplyService _heatEnergyService;
    private readonly IElectricitySupplyService _electrSupplyService;

    public FullChargeService(ICWSupplyService coldWaterService,
        IHCSupplyService heatCarrierService, IHESupplyService heatEnergyService,
        IElectricitySupplyService electrSupplyService,
        IRepository<FullMeasurement> measRep, IRepository<User> userRep)
    {
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

    private void GetElectrAmount(FullChargeViewModel model, MeasurementViewModel meas, int numOfRes,
        FullMeasurement pastMeas)
    {
        if (meas.DailyElectricityAmount != 0 && meas.NightElectricityAmount != 0)
        {
            model.DailyElectricityCharge = _electrSupplyService.CountChargeForDays(
                meas.DailyElectricityAmount,
                pastMeas.DailyElectricityAmount);

            model.NightElectricityCharge = _electrSupplyService.CountChargeForNights(
                meas.NightElectricityAmount,
                pastMeas.NightElectricityAmount);
            model.FullPayment += model.DailyElectricityCharge;
            model.FullPayment += model.NightElectricityCharge;
        }
        else
        {
            model.ElectricityCharge = _electrSupplyService.CountCharge(numOfRes);
            model.FullPayment += model.ElectricityCharge;
        }
    }

    public FullChargeViewModel CalcCharge(MeasurementViewModel measurement, string userId)
    {
        var user = _userRep.GetById(userId);
        var pastMeas = GetPastMeas(userId, measurement.MeasurementMonth);
        var fullCharge = new FullChargeViewModel
        {
            FullPayment = 0
        };
        GetElectrAmount(fullCharge, measurement, user.NumberOfResidents, pastMeas);

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