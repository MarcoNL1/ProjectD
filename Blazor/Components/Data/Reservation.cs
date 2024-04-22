public class Reservation
{
    public User User { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public Reservation(User user, DateTime startDate, DateTime endDate)
    {
        User = user;
        StartDate = startDate;
        EndDate = endDate;
    }
}