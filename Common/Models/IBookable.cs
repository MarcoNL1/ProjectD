namespace Common.Models;

public interface IBookable
{
    Guid Id { get; set; }
    public int FloorNumber { get; }
    public string Wing { get; }
    public int RoomNumber { get; }
}