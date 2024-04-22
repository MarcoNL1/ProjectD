namespace Blazor.Data;

public class WorkspaceRoom : IBookable
{
    public int FloorNumber { get; }
    public int RoomNumber { get; }
    private List<Workspace> workspaces = new List<Workspace>();
    private Workspace selectedWorkspace;

    public WorkspaceRoom(int floorNumber, int roomNumber)
    {
        FloorNumber = floorNumber;
        RoomNumber = roomNumber;
    }

    public void AddWorkspace(Workspace workspace)
    {
        workspaces.Add(workspace);
    }
    
    public void RemoveWorkspace(Workspace workspace)
    {
        workspaces.Remove(workspace);
    }

    public bool IsAvailable(DateTime startDate, DateTime endDate)
    {
        List<Workspace> availableWorkspaces = new List<Workspace>();
        foreach (var workspace in workspaces)
        {
            if (workspace.IsAvailable(startDate, endDate))
            {
                availableWorkspaces.Add(workspace);
            }
        }
        return true;
    }

    public bool Book(User user, DateTime startDate, DateTime endDate)
    {
        if (selectedWorkspace != null)
        {
            Reservation newReservation = new Reservation(user, startDate, endDate);
            selectedWorkspace.Reserve(newReservation);
            // user.Book(selectedWorkspace);
            Console.WriteLine($"Reservation for {selectedWorkspace.DeskName} has been made successfully.");
            // Reset selected workspace after booking
            selectedWorkspace = null;
            return true;
        }
        else
        {
            Console.WriteLine("No workspace selected for booking.");
            return false;
        }
    }

    public void SelectWorkspace(Workspace workspace)
    {
        selectedWorkspace = workspace;
    }
}