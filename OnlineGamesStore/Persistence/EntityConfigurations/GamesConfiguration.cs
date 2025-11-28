using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineGamesStore.Models;

namespace OnlineGamesStore.Persistence.EntityConfigurations
{
    public class GamesConfiguration : IEntityTypeConfiguration<Games>
    {
        public void Configure(EntityTypeBuilder<Games> builder)
        {
            builder.ToTable("Games");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name).IsRequired().HasMaxLength(250);
            builder.Property(g => g.Description).HasMaxLength(2000);
            builder.Property(g => g.Genre).HasMaxLength(100);
            builder.Property(g => g.Price).HasColumnType("decimal(18,2)");
            builder.Property(g => g.Platform).HasMaxLength(100);
        }
       
    }
}
