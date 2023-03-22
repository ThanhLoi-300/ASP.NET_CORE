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
    public class Product_Configuration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Product");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description)
                .HasMaxLength(255);
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .IsFixedLength().IsRequired();
            entity.Property(e => e.IsDeleted);
            entity.Property(e => e.Name)
                .HasMaxLength(255).IsRequired();
            entity.Property(e => e.Price);
            entity.Property(e => e.QuantityL).IsRequired();
            entity.Property(e => e.QuantityM).IsRequired();
            entity.Property(e => e.QuantityS).IsRequired();
            entity.Property(e => e.QuantityXl).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.Type).IsRequired();

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.Category_ID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");
        }
    }
}
