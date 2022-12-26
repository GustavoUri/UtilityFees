using Microsoft.AspNetCore.Identity;

namespace UtilityFees.Data.Entities;

public class User : IdentityUser
{
    public int NumberOfResidents { get; set; }
    public bool HasCWSDevice { get; set; }
    public bool HasHWSDevice { get; set; }
    public bool HasPSDevice { get; set; }
    public IEnumerable<FullMeasurement> Measurements;
}