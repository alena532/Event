using System.Reflection.Metadata.Ecma335;
using Events.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Events.DBContext;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AppDbContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<AppDbContext>>()))
        {
            
            if (context.Roles.Any()) return;
            
            context.Roles.AddRange(
                new Role(){Name = "Admin"},
                new Role(){Name = "CompanyWorker"}
                );
            
            if (context.Companies.Any()) return;
            
            context.Companies.AddRange(
                new Company(){Name="MakingCelebrations"},
                new Company(){Name="Let`sCelebrate"},
                new Company(){Name="Celebrations"}
                );
            context.SaveChanges();

            if (context.Speakers.Any()) return;
            context.Speakers.AddRange(
                new Speaker(){FirstName = "Alena",LastName = "Vorobey"},
                new Speaker(){FirstName = "Kristina",LastName = "Vorobey"});
           
            context.SaveChanges();
            
        }
    }
}