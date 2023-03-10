using Microsoft.AspNetCore.Identity;
using UtilityFees.BusinessLogic.Interfaces;
using UtilityFees.BusinessLogic.ViewModels;
using UtilityFees.Data.Entities;

namespace UtilityFees.BusinessLogic.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<User> _signInManager;

    public AuthenticationService(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }


    public async Task LoginAsync(LoginViewModel model)
    {
        await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}