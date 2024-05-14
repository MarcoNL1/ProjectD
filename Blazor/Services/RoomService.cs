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

    public async Task<Room?> GetRoomByIdAsync(Guid id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Room> CreateRoomAsync(Room room)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        await context.Rooms.AddAsync(room);
        await context.SaveChangesAsync();
        return room;
    }

    public async Task<Room?> UpdateRoomAsync(Room room)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var existingRoom = await context.Rooms.FirstOrDefaultAsync(r => r.Id == room.Id);
        if (existingRoom == null) return null;
        existingRoom.FloorNumber = room.FloorNumber;
        existingRoom.Wing = room.Wing;
        existingRoom.RoomNumber = room.RoomNumber;
        existingRoom.Type = room.Type;
        existingRoom.Name = room.Name;
        existingRoom.MaxWorkspaces = room.MaxWorkspaces;
        existingRoom.MaxReservations = room.MaxReservations;
        await context.SaveChangesAsync();
        return existingRoom;
    }

    public async Task DeleteRoomAsync(Guid id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var room = await context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
        if (room is not null)
        {
            context.Rooms.Remove(room);
            await context.SaveChangesAsync();
        }
    }
}