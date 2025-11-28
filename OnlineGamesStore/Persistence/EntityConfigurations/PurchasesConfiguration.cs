using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGamesStore.Models;

namespace OnlineGamesStore.Persistence.EntityConfigurations
{
    public class PurchasesConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchases");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PurchaseDate).IsRequired();
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18,2)");


            builder.HasOne(p => p.Games).WithMany(g => g.Purchases).HasForeignKey(p => p.GameId);
            builder.HasOne(p => p.Users).WithMany(u => u.Purchases).HasForeignKey(p => p.UserId);
        }
    }
}
