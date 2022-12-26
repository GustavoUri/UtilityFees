using Microsoft.AspNetCore.Identity;
using UtilityFees.BusinessLogic.Interfaces;
using UtilityFees.BusinessLogic.Services;
using UtilityFees.Data.AppEFContext;
using Microsoft.EntityFrameworkCore;
using UtilityFees.Data.Entities;
using UtilityFees.Data.Interfaces;
using UtilityFees.Data.Repositories;

namespace UtilityFees;

public static class ServiceExtensions
{
    public static void ConfigureCustomServices(this IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserOptionsService, UserOptionsService>();
        services.AddScoped<IRepository<User>, UserRepository>();
        services.AddScoped<IMeasurementService, MeasurementService>();
        services.AddScoped<IRepository<FullMeasurement>, MeasurementRepository>();
        services.AddScoped<ICWSupplyService, ColdWaterSupplyService>();
        services.AddScoped<IElectricitySupplyService, ElectricitySupplyService>();
        services.AddScoped<IFullChargeService, FullChargeService>();
        services.AddScoped<IHCSupplyService, HeatCarrierService>();
        services.AddScoped<IHESupplyService, HeatEnergyService>();
    }

    public static void ConfigureDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=db.db"));
    }
    
}