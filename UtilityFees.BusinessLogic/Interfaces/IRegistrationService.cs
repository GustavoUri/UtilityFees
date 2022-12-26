using UtilityFees.BusinessLogic.ViewModels;

namespace UtilityFees.BusinessLogic.Interfaces;

public interface IRegistrationService
{
    Task<bool> RegisterAsync(RegisterViewModel register);
}