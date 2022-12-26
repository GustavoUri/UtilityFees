using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UtilityFees.BusinessLogic.Interfaces;
using UtilityFees.BusinessLogic.ViewModels;
using UtilityFees.Data.Entities;

namespace UtilityFees.Controllers;

public class ChargesController : Controller
{
    private readonly IFullChargeService _chargeService;
    private readonly UserManager<User> _userManager;
    private readonly IMeasurementService _measService;

    public ChargesController(IFullChargeService chargeService, UserManager<User> userManager,
        IMeasurementService measService)
    {
        _chargeService = chargeService;
        _userManager = userManager;
        _measService = measService;
    }

    public async Task<IActionResult> Charges(MeasurementViewModel chargeViewModel)
    {
        if (!User.Identity.IsAuthenticated)
            return Redirect("~/Account/Login");
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        _measService.AddMeasurement(user.Id, chargeViewModel);
        var charge = _chargeService.CalcCharge(chargeViewModel, user.Id);
        return View(charge);
    }
}