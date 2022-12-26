namespace UtilityFees.BusinessLogic.Interfaces;

public interface ICWSupplyService
{
    double CalcCharge(double indications, double pastIndications);
    double CalcCharge(int numOfResidents);
    double CalcVolume(int numOfResidents);
    double CalcVolume(double indications, double pastIndications);
}