

using Events.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Events.DBContext;


public class AppDbContext : IdentityDbContext<User,Role,string>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    
    

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>(entity =>
        {

            entity.HasOne(e => e.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

        });
        
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasOne(e => e.Company)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Speaker)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.SpeakerId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasIndex(e=>e.Name)
                .IsUnique();
        });
        
        base.OnModelCreating(modelBuilder);
    }

}