namespace Blazor.Components;

public class MeetingRoom : Room
{
    public int Seats { get; }

    public MeetingRoom(int floorNumber, int roomNumber, int seats) 
        : base(floorNumber, roomNumber) 
    {
        Seats = seats;
    }
}