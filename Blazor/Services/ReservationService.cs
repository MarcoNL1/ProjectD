using Blazor.Data;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Services;

public class ReservationService(IDbContextFactory<AppDbContext> contextFactory)
{
    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.Reservations.ToListAsync();
    }
}