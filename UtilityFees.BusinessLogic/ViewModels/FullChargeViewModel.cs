namespace UtilityFees.BusinessLogic.ViewModels;

public class FullChargeViewModel
{
    public double DailyElectricityCharge { get; set; }
    public double NightElectricityCharge { get; set; }
    public double ElectricityCharge { get; set; }
    public double HeatCarrierCharge { get; set; }
    public double HeatEnergyCharge { get; set; }
    public double ColdWaterCharge { get; set; }
    public double FullPayment { get; set; }
}