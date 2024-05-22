using Microsoft.AspNetCore.Identity;

namespace Blazor.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public bool IsAdmin = false;

    public IEnumerable<Reservation> Reservations { get; } = new List<Reservation>();
    public Reservation Book(IBookable bookable, DateTime startDate, DateTime endDate)
    {
        return new Reservation();
    }
}