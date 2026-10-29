using EFCore02.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore02.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Organizer> Organizers => Set<Organizer>();
        public DbSet<OrganizerProfile> OrganizerProfiles => Set<OrganizerProfile>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Attendee> Attendees => Set<Attendee>();
        public DbSet<Badge> Badges => Set<Badge>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=EventHubDb;Trusted_Connection=True;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrganizerProfile>(OP =>
            {
                OP.ToTable("OrganizerProfiles");
                OP.HasKey(p => p.OrganizerId);

                OP.Property(p => p.Biography).HasMaxLength(1000);
                OP.Property(p => p.WebsiteUrl).HasMaxLength(300);
                OP.Property(p => p.LogoUrl).HasMaxLength(500);

                OP.HasOne(p => p.Organizer)
                      .WithOne(o => o.Profile)
                      .HasForeignKey<OrganizerProfile>(p => p.OrganizerId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
