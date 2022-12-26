using Microsoft.AspNetCore.Mvc;
using UtilityFees.BusinessLogic.Interfaces;
using UtilityFees.BusinessLogic.ViewModels;

namespace UtilityFees.Controllers;

public class AccountController : Controller
{
    private readonly IRegistrationService _registrationService;
    private readonly IAuthenticationService _authenticationService;

    public AccountController(IRegistrationService registrationService, IAuthenticationService authenticationService)
    {
        _registrationService = registrationService;
        _authenticationService = authenticationService;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _registrationService.RegisterAsync(model);
            if (!result)
                return View(model);
        }

        return RedirectToAction("UserOptions", "UserOptions");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        await _authenticationService.LoginAsync(model);
        return RedirectToAction("UserMeasurements", "Measurements");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _authenticationService.LogoutAsync();
        return RedirectToAction("Login", "Account");
    }
}