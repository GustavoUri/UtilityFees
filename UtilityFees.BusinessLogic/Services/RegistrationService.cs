using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UtilityFeesApp.BusinessLogic.Interfaces;
using UtilityFeesApp.BusinessLogic.ViewModels;
using UtilityFeesAppData.Entities;
using UtilityFeesAppData.Interfaces;

namespace UtilityFeesApp.BusinessLogic.Services;

public class RegistrationService : IRegistrationService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public RegistrationService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> RegisterAsync(RegisterViewModel register)
    {
        var user = new User {UserName = register.Login};
        var result = await _userManager.CreateAsync(user, register.Password);
        if (!result.Succeeded) return false;
        await _signInManager.SignInAsync(user, false);
        return true;

    }
}