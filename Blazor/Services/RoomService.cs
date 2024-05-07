using Blazor.Data;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Services;

public class RoomService(IDbContextFactory<AppDbContext> contextFactory)
{
    public async Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.Rooms.ToListAsync();
    }
}