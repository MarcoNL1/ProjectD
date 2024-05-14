using System.ComponentModel.DataAnnotations;

namespace Blazor.Data;

public class Room : IBookable
{
    [Key] public Guid Id { get; set; }
    [Required] public int FloorNumber { get; set; }
    [Required] [MaxLength(1)] public string Wing { get; set; }
    [Required] public int RoomNumber { get; set; }
    [MaxLength(20)] public string Type { get; set; }
    [MaxLength(20)] public string Name { get; set; } = "";
    public uint MaxWorkspaces { get; set; }
    public uint MaxReservations { get; set; }

    public bool HasWorkspaces => MaxWorkspaces > 0;
    public bool IsBookable => MaxReservations > 0;

    private List<Reservation> reservations = new List<Reservation>();

    public bool IsAvailable(DateTime startDate, DateTime endDate)
    {
        foreach (var reservation in reservations)
        {
            if (startDate < reservation.EndDate && endDate > reservation.StartDate)
            {
                return false; // =occupied
            }
        }

        return true;
    }

    public bool Book(User user, DateTime startDate, DateTime endDate)
    {
        // Implement booking logic here
        if (IsAvailable(startDate, endDate))
        {
            // Create a new reservation
            var newReservation = new Reservation
            {
                User = user,
                Room = this,
                StartDate = startDate,
                EndDate = endDate
            };

            reservations.Add(newReservation);
            return true; // Booking successful
        }
        else
        {
            return false; // Booking failed (room not available/occupied)
        }
    }
}