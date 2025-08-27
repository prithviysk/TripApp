using Microsoft.EntityFrameworkCore;
using TripApp.Models;

namespace TripApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Trip> Trips { get; set; }
    public DbSet<Stop> Stops { get; set; }
}