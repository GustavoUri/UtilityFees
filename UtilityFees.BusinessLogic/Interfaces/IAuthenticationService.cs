using UtilityFees.BusinessLogic.ViewModels;

namespace UtilityFees.BusinessLogic.Interfaces;

public interface IAuthenticationService
{
    Task LoginAsync(LoginViewModel model);
    Task LogoutAsync();
}