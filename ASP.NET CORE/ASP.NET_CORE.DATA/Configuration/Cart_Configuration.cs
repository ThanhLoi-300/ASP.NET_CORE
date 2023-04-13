using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASP.NET_CORE.DATA.Configuration
{
    public class Cart_Configuration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> entity)
        {
            entity.ToTable("Cart");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Price).IsRequired();
            entity.Property(e => e.ProductId);
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.Size).IsRequired();
            entity.Property(e => e.ClientId);

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_Product");

            entity.HasOne(d => d.Client).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_Client");
        }
    }
}
