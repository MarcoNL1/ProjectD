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
    public int Count { get; set; } = 0;
    public List<Reservation> Reservations { get; set; }
    
    public bool HasWorkspaces => MaxWorkspaces > 0;
    public bool IsBookable => MaxReservations > 0;
    public string RoomCode => $"{FloorNumber:00}.{Wing}.{RoomNumber}";
    public bool IsAvailable(DateTime startDate, DateTime endDate) => Reservations.Count < MaxReservations;
}