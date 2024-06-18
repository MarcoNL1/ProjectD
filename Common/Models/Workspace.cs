using System.ComponentModel.DataAnnotations;

namespace Common.Models;

public class Workspace : IBookable
{
    [Key] public Guid Id { get; set; }
    public int FloorNumber => Room.FloorNumber;
    public string Wing => Room.Wing;
    public int RoomNumber => Room.RoomNumber;

    public Guid RoomId { get; set; }
    public Room Room { get; set; } = null!;
    public string DeskName { get; set; }
    public List<Reservation> Reservations = [];
}