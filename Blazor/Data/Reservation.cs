using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Data;

public class Reservation
{
    [Key] public Guid Id { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; } = null!;

    private Room? _room;

    [NotMapped]
    public Room? Room
    {
        private get => _room;
        set
        {
            if (Workspace == null) _room = value;
        }
    }

    private Workspace? _workspace;

    [NotMapped]
    public Workspace? Workspace
    {
        private get => _workspace;
        set
        {
            if (Room == null) _workspace = value;
        }
    }

    [NotMapped] public IBookable? Bookable => (IBookable?)Room ?? (IBookable?)Workspace;
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
}