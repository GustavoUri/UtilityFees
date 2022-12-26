using UtilityFees;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCustomServices();
builder.Services.ConfigureDatabase();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Measurements}/{action=UserMeasurements}");

app.Run();