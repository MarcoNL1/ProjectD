namespace Blazor.Components;

public class Workspace
{
    public string DeskName { get; }
    private List<Reservation> reservations = new List<Reservation>();

    public Workspace(string deskName)
    {
        DeskName = deskName;
    }

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

    public void Reserve(Reservation reservation)
    {
        reservations.Add(reservation);
    }
}