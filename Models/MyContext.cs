#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace weddingPlannerZied.Models;
public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Wedding> Weddings { get; set; }
    // public DbSet<Guest> Guests { get; set; }
}