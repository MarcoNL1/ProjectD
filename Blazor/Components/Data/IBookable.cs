namespace Blazor.Components;

public interface IBookable
{
    bool IsAvailable(DateTime startDate, DateTime endDate);
    bool Book(User user, DateTime startDate, DateTime endDate);
}