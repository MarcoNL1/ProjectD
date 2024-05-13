using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Workspace> Workspaces { get; init; } = default!;
    public DbSet<Room> Rooms { get; init; } = default!;
    public DbSet<Reservation> Reservations { get; init; } = default!;
}