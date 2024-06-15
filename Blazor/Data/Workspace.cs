using System.ComponentModel.DataAnnotations;
using Blazor.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Data;

public class Workspace : IBookable
{
    [Key] public Guid Id { get; set; }
    public string DeskName { get; }
    public List<Reservation> Reservations = [];

    public bool IsAvailable(DateTime startDate, DateTime endDate)
    {
        foreach (var reservation in Reservations)
        {
            if (startDate < reservation.EndDate && endDate > reservation.StartDate)
            {
                return false;
            }
        }

        return true;
    }
}