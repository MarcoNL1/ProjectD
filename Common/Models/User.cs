using Microsoft.AspNetCore.Identity;

namespace Common.Models;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public IEnumerable<Reservation> Reservations { get; } = new List<Reservation>();
}