using System.Reflection.Metadata.Ecma335;
using Events.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Events.DBContext;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new EventsContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<EventsContext>>()))
        {
           /* if (context.Roles.Any()) return;
            
            context.Roles.AddRange(
                new IdentityRole("Admin"),
                new IdentityRole("CompanyWorker")
                );
            
            context.Companies.AddRange(
                new Company(){Name="MakingCelebrations"},
                new Company(){Name="Let`sCelebrate"},
                new Company(){Name="Celebrations"}
                );
            context.SaveChanges();
            var firstCompany = context.Companies.Find(1);
            */
           //var firstCompany = context.Companies.Find(1);
           // context.Users.AddRange(
                //new User(){FirstName = "Anton",LastName = "Lechenkov",Company = firstCompany}
           //     new User(){FirstName = "Anton",LastName = "Lechenko"}
           //         );
            context.Speakers.AddRange(
                new Speaker(){FirstName = "Alena",LastName = "Vorobey"},
                new Speaker(){FirstName = "Kristina",LastName = "Vorobey"}
                );
            context.SaveChanges();
        }
    }
}