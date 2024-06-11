using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Data;

public class Reservation
{
    [Key] public Guid Id { get; set; }
    
    public string UserId { get; set; }
    public User User { get; init; } = null!;

    private Guid? _roomId;
    public Guid? RoomId 
    {
        get => _roomId;
        set
        {
            if (Workspace == null) _roomId = value;
        }
    }
    public Room? Room { get; set; }

    private Guid? _workspaceId;
    public Guid? WorkspaceId
    {
        get => _workspaceId;
        set
        {
            if (Room == null) _workspaceId = value;
        }
    }
    public Workspace? Workspace { get; set; }

    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }

    [NotMapped] public IBookable? Bookable
    {
        get => (IBookable?)Room ?? Workspace;
        set {
            switch (value)
            {
                case Room room:
                    RoomId = value.Id;
                    break;
                case Workspace workspace:
                    WorkspaceId = value.Id;
                    break;
            }
        }
    }

    public bool SetBookable(Guid bookableId, string type)
    {
        switch (type.ToLower())
        {
            case "room":
                RoomId = bookableId;
                return true;
            case "workspace":
                WorkspaceId = bookableId;
                return true;
            default:
                return false;
        }
    }
    public bool SetBookable(IBookable bookable, string type)
    {
        return SetBookable(bookable.Id, type);
    }
    
    public string FormatTimeRange()
    {
        return $"{(DateTime.Today.Year != StartDate.Year ? StartDate.Year : string.Empty )} {StartDate:dddd, MMMM d} {StartDate:HH:mm} - {(DateTime.Today.Year != EndDate.Year ? EndDate.Year : string.Empty )} {(StartDate.Date == EndDate.Date ? string.Empty : $"{EndDate:dddd, MMMM d}")} {EndDate:HH:mm}";
    }
}