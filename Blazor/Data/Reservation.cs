using System.ComponentModel.DataAnnotations;

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
    
    public IBookable? Bookable => (IBookable?)Room ?? Workspace;
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
        return $"{StartDate:dddd, MMMM d} {StartDate:HH:mm} - {(StartDate.Date == EndDate.Date ? string.Empty : $"{EndDate:dddd, MMMM d}")} {EndDate:HH:mm}";
    }
}