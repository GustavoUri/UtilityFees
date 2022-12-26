using System.ComponentModel.DataAnnotations;

namespace UtilityFees.Data.Entities;

public class FullMeasurement
{
    [Key] public int Id { get; set; }
    public double DailyElectricityAmount { get; set; }
    public double NightElectricityAmount { get; set; }
    public double HotWaterAmount { get; set; }
    public double ColdWaterAmount { get; set; }
    public int MeasurementMonth { get; set; }
    public User? User { get; set; }
    public string? UserId { get; set; }
}