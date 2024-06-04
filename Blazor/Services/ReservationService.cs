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

    public async Task<Reservation?> GetReservationByIdAsync(Guid id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<Reservation>> GetAllReservationsByUserId(string userId, bool withBookable = false)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        if (withBookable)
            return await context.Reservations.Where(r => r.UserId == userId)
                .Include(r => r.Room)
                .Include(r => r.Workspace)
                .ToListAsync();
        return await context.Reservations.Where(r => r.UserId == userId).ToListAsync();
    }

    public async Task<Reservation> CreateReservationAsync(Reservation reservation)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        await context.Reservations.AddAsync(reservation);
        await context.SaveChangesAsync();
        return reservation;
    }

    public async Task DeleteReservationAsync(Guid id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var reservation = await context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
        if (reservation is not null)
        {
            context.Reservations.Remove(reservation);
            await context.SaveChangesAsync();
        }
    }
}