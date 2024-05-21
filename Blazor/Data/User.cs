using Microsoft.AspNetCore.Identity;

namespace Blazor.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public bool IsAdmin = false;
    
    public List<Reservation> Reservations { get; set; }
    public Reservation Book(IBookable bookable, DateTime startDate, DateTime endDate)
    {
        return new Reservation();
    }
}