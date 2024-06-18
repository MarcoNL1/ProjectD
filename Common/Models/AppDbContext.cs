using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Common.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Workspace> Workspaces { get; init; } = default!;
    public DbSet<Room> Rooms { get; init; } = default!;
    public DbSet<Reservation> Reservations { get; init; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .HasMany(u => u.Reservations)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .IsRequired();

        builder.Entity<Reservation>()
            .HasOne(r => r.Room)
            .WithMany(r => r.Reservations)
            .HasForeignKey(r => r.RoomId);

        builder.Entity<Reservation>()
            .HasOne(r => r.Workspace)
            .WithMany(r => r.Reservations)
            .HasForeignKey(r => r.WorkspaceId);

        builder.Entity<Workspace>()
            .HasOne(w => w.Room)
            .WithMany(r => r.Workspaces)
            .HasForeignKey(w => w.RoomId);
    }
}