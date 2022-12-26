namespace UtilityFees.BusinessLogic.Interfaces;

public interface IHESupplyService
{
    double CalcCharge(double indications, double pastIndications);
    double CalcCharge(int numOfResidents);
    
}