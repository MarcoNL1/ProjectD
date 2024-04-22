namespace Blazor.Data;

public class MeetingRoom : IBookable
{
    public int FloorNumber { get; }
    public int RoomNumber { get; }

    public MeetingRoom(int floorNumber, int roomNumber)
    {
        FloorNumber = floorNumber;
        RoomNumber = roomNumber;
    }
    
    private List<Reservation> reservations = new List<Reservation>();
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
    
    public bool Book(User user, DateTime startDate, DateTime endDate)
    {
        if (IsAvailable(startDate, endDate))
        {
            Reservation newReservation = new Reservation(user, startDate, endDate);
            reservations.Add(newReservation);
            // Link the user to the room
            // user.Book(this);
            Console.WriteLine("Reservation has been made successfully.");
            return true; // Booking successful
        }
        else
        {
            Console.WriteLine("This room is not available for reservation.");
            return false; // Booking failed
        }
    }
}