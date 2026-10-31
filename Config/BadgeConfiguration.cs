using EFCore02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore02.Config
{
    public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
    {
        public void Configure(EntityTypeBuilder<Badge> builder)
        {
            builder.ToTable("Badges");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.BadgeNumber)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.HasIndex(b => b.BadgeNumber).IsUnique();

            builder.Property(b => b.IssuedDate)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(b => b.Tier)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(20);

            builder.HasOne(b => b.Attendee)
                   .WithOne(a => a.Badge)
                   .HasForeignKey<Badge>(b => b.AttendeeId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
