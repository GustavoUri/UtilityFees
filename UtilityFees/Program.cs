using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UtilityFees.BusinessLogic.Interfaces;
using UtilityFees.BusinessLogic.Services;
using UtilityFees.Data.AppEFContext;
using UtilityFeesApp.BusinessLogic.Interfaces;
using UtilityFeesApp.BusinessLogic.Services;
using UtilityFeesApp.BusinessLogic.ViewModels;
using UtilityFeesAppData.Entities;
using UtilityFeesAppData.Interfaces;
using UtilityFeesAppData.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123"));
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserOptionsService, UserOptionsService>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<IRepository<FullMeasurement>, MeasurementRepository>();
builder.Services.AddScoped<ICWSupplyService, ColdWaterSupplyService>();
builder.Services.AddScoped<IElectricitySupplyService, ElectricitySupplyService>();
builder.Services.AddScoped<IFullChargeService, FullChargeService>();
builder.Services.AddScoped<IHCSupplyService, HeatCarrierService>();
builder.Services.AddScoped<IHESupplyService, HeatEnergyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Measurements}/{action=UserMeasurements}");

app.Run();