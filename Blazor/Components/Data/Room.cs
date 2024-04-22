namespace Blazor.Components;

public class Room
{
    public int ID { get; }
    public int FloorNumber { get; }
    public int RoomNumber { get;  }
    public string Type { get; }
    
    public Room(int floorNumber, int roomNumber, string type = "default")
    {
        FloorNumber = floorNumber;
        RoomNumber = roomNumber;
        Type = type;
        ID = 1;
    }
}