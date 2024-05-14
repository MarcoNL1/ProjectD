using System.ComponentModel.DataAnnotations;
using Blazor.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Data;

public class Workspace : IBookable
{
    [Key] public Guid Id { get; set; }
    public string DeskName { get; }
    private List<Reservation> reservations = [];

    public bool IsAvailable(DateTime startDate, DateTime endDate)
    {
        foreach (var reservation in reservations)
        {
            if (startDate < reservation.EndDate && endDate > reservation.StartDate)
            {
                return false;
            }
        }

        return true;
    }

    public bool Book(User user, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }
}