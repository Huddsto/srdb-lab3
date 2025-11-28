using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineGamesStore.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineGamesStore.Persistence.EntityConfigurations
{
    public class DevelopersConfiguration : IEntityTypeConfiguration<Developers>
    {
        public void Configure(EntityTypeBuilder<Developers> builder)
        {
            builder.ToTable("Developers");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(200);
            builder.Property(d => d.Country).HasMaxLength(200);
            builder.Property(d => d.Website).HasMaxLength(300);
            builder.Property(d => d.Information).HasMaxLength(2000);

            builder.HasMany(d => d.Games).WithOne(g => g.Developer).HasForeignKey(g => g.DeveloperId);
        }
    }
}
