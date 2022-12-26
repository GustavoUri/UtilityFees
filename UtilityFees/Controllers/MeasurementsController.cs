using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UtilityFees.BusinessLogic.ViewModels;
using UtilityFees.Data.Entities;

namespace UtilityFees.Controllers;

public class MeasurementsController : Controller
{
    private readonly UserManager<User> _userManager;

    public MeasurementsController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> UserMeasurements()
    {
        if (!User.Identity.IsAuthenticated)
            return Redirect("~/Account/Login");
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        ViewBag.Options = new UserOptionsViewModel()
        {
            HasPSDevice = user.HasPSDevice,
            HasCWSDevice = user.HasCWSDevice,
            HasHWSDevice = user.HasHWSDevice
        };
        return View();
    }
}