namespace Blazor.Components;

public class Workspace : Room
{
    public int Seats { get; }

    public Workspace(int floorNumber, int roomNumber, int seats)
        : base(floorNumber, roomNumber) 
    {
        Seats = seats;
    }
}