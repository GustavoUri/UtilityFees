namespace UtilityFees.BusinessLogic.Interfaces;

public interface ISupplyService
{
    double CalcCharge(double indications, double pastIndications);
    double CalcCharge(int numOfResidents);
    double CalcVolume(int numOfResidents);
    double CalcVolume(double indications, double pastIndications);
}