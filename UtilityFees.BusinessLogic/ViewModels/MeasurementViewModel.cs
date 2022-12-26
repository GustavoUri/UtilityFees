namespace UtilityFees.BusinessLogic.ViewModels;

public class MeasurementViewModel
{
    public double DailyElectricityAmount { get; set; }
    public double NightElectricityAmount { get; set; }
    public double HotWaterAmount { get; set; }
    public double ColdWaterAmount { get; set; }
    public int MeasurementMonth { get; set; }
}