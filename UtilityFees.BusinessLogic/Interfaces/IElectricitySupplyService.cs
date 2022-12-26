namespace UtilityFees.BusinessLogic.Interfaces;

public interface IElectricitySupplyService
{
    double CountCharge(int numOfResidents);
    double CountVolume(int numOfResidents);
    double CountVolume(double indications, double pastIndications);
    double CountChargeForDays(double indications, double pastIndications);
    double CountChargeForNights(double indications, double pastIndications);
}