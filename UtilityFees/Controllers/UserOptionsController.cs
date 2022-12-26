using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UtilityFeesApp.BusinessLogic.Interfaces;
using UtilityFeesApp.BusinessLogic.ViewModels;
using UtilityFeesAppData.Entities;
using UtilityFeesAppData.Interfaces;

namespace UtilityFees.Controllers;

public class UserOptionsController : Controller
{
    private readonly IRepository<User> _userRep;
    private readonly UserManager<User> _userManager;
    private readonly IUserOptionsService _optionsService;
    public UserOptionsController(IRepository<User> userRep, UserManager<User> userManager, IUserOptionsService optionsService)
    {
        _userRep = userRep;
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
        return RedirectToAction("UserMeasurements","Measurements");
    }
    
}