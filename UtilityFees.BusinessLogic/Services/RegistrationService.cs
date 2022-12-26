using Microsoft.AspNetCore.Identity;
using UtilityFees.BusinessLogic.Interfaces;
using UtilityFees.BusinessLogic.ViewModels;
using UtilityFees.Data.Entities;

namespace UtilityFees.BusinessLogic.Services;

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