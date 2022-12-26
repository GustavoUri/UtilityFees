using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UtilityFees.BusinessLogic.Interfaces;
using UtilityFees.BusinessLogic.ViewModels;
using UtilityFees.Data.Entities;

namespace UtilityFees.Controllers;

public class UserOptionsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IUserOptionsService _optionsService;

    public UserOptionsController(UserManager<User> userManager, IUserOptionsService optionsService)
    {
        _userManager = userManager;
        _optionsService = optionsService;
    }

    // GET
    public IActionResult UserOptions()
    {
        if (!User.Identity.IsAuthenticated)
            return Redirect("~/Account/Login");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UserOptions(UserOptionsViewModel model)
    {
        if (!User.Identity.IsAuthenticated)
            return Redirect("~/Account/Login");
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        _optionsService.SetUserOptions(model, user.Id);
        return RedirectToAction("UserMeasurements", "Measurements");
    }
}