

using Events.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Events.DBContext;


public class EventsContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    
    

    public EventsContext(DbContextOptions<EventsContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    //  connect to postgres with connection string from app settings
    //modelBuilder.UseNpgsql(Configuration.GetConnectionString("Events"));
    // base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Company>().HasIndex(u => u.Name).IsUnique();
        
        modelBuilder.UseSerialColumns();
    }

}