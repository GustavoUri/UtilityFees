using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UtilityFees.BusinessLogic.Interfaces;
using UtilityFeesApp.BusinessLogic.ViewModels;
using UtilityFeesAppData.Entities;

namespace UtilityFees.Controllers;

public class MeasurementsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IMeasurementService _measService;
    public MeasurementsController(UserManager<User> userManager, IMeasurementService measService)
    {
        _userManager = userManager;
        _measService = measService;
    }
    //[Authorize]
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

    [HttpPost]
    public async Task<IActionResult> GetMeasurements(MeasurementViewModel model)
    {
        if (!User.Identity.IsAuthenticated)
            return Redirect("~/Account/Login");
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        _measService.AddMeasurement(user.Id, model);
        return RedirectToAction("Index", "Home");
        
    }
    
    
}