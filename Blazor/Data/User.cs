using Microsoft.AspNetCore.Identity;

namespace Blazor.Data;

// Add profile data for application users by adding properties to the User class
public class User(string name) : IdentityUser
{
    public bool IsAdmin = false;
    public string Name = name;

    public Reservation Book(IBookable bookable, DateTime startDate, DateTime endDate)
    {
        return new Reservation();
    }
}