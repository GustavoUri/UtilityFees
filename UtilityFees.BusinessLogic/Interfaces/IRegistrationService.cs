using Microsoft.AspNetCore.Identity;
using UtilityFeesApp.BusinessLogic.ViewModels;

namespace UtilityFeesApp.BusinessLogic.Interfaces;

public interface IRegistrationService
{
    Task<bool> RegisterAsync(RegisterViewModel register);
}