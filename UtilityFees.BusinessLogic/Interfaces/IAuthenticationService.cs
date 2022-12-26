using UtilityFeesApp.BusinessLogic.ViewModels;

namespace UtilityFeesApp.BusinessLogic.Interfaces;

public interface IAuthenticationService
{
    Task LoginAsync(LoginViewModel model);
    Task LogoutAsync();
}