using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UtilityFeesAppData.Entities;

namespace UtilityFees.Data.AppEFContext;

public class AppDbContext : IdentityDbContext<User>
{
    public DbSet<FullMeasurement> Measurements => Set<FullMeasurement>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123");
    }
}