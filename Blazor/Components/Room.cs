namespace Blazor.Components;

public class Room
{
    public int FloorNumber { get; }
    public int RoomNumber { get;  }

    public Room(int floorNumber, int roomNumber)
    {
        FloorNumber = floorNumber;
        RoomNumber = roomNumber;
    }
}