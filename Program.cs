using EFCore02.Contexts;
using EFCore02.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Create console app with all entity classes

            //Create console app with all entity classes

            #endregion

            #region Create DbContext and OnModelCreating and config directly in OnModelCreating

            //Create DbContext and OnModelCreating and config directly in OnModelCreating

            #endregion

            #region config in separate configuration classes and Add-Migration

            //config in separate configuration classes and Add-Migration

            #endregion

            #region Demonstrate that the database is created correctly

            //Demonstrate that the database is created correctly

            using var context = new AppDbContext();
            context.Database.Migrate();

            if (!context.Organizers.Any())
            {
                var organizer = new Organizer
                {
                    Name = "Sara Ahmed",
                    CompanyName = "BrightEvents",
                    IsVerified = true,
                    Profile = new OrganizerProfile
                    {
                        Biography = "We organize tech meetups.",
                        WebsiteUrl = "https://brightevents.example",
                        LogoUrl = "https://brightevents.example/logo.png"
                    },
                    Events =
        {
            new Event
            {
                Title = "EF Core Workshop",
                Description = "Hands-on EF Core session.",
                StartDate = DateTime.Today.AddDays(14),
                MaxAttendees = 100
            }
        }
                };

                var attendee = new Attendee
                {
                    FullName = "Omar Hassan",
                    Email = "omar@example.com",
                    Street = "12 Nile St",
                    City = "Zagazig",
                    Country = "Egypt",
                    PostalCode = "44519",
                    Badge = new Badge
                    {
                        BadgeNumber = "B-0001",
                        IssuedDate = DateTime.Today,
                        Tier = BadgeTier.VIP
                    }
                };

                context.AddRange(organizer, attendee);
                context.SaveChanges();
            }

            foreach (var o in context.Organizers.Include(o => o.Profile).Include(o => o.Events))
                Console.WriteLine($"{o.Name} ({o.CompanyName}) verified={o.IsVerified} events={o.Events.Count} site={o.Profile?.WebsiteUrl}");

            foreach (var a in context.Attendees.Include(a => a.Badge))
                Console.WriteLine($"{a.FullName} | {a.FullAddress} | badge={a.Badge?.BadgeNumber} {a.Badge?.Tier}");
            

            #endregion

        }
    }
}
