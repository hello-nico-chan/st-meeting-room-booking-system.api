using MeetingRoomBookingSystem.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBookingSystem.Persistence;

public class MrbsDbContext : DbContext
{
    public DbSet<MeetingRoom> MeetingRooms { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }

    public MrbsDbContext(DbContextOptions<MrbsDbContext> options) : base(options)
    {

    }
}
