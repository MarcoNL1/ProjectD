namespace Blazor.Components;

public class Room
{
    public int FloorNumber { get; }
    public string RoomType { get; }

    public Room(int floorNumber, string roomType)
    {
        FloorNumber = floorNumber;
        RoomType = roomType;
    }
}