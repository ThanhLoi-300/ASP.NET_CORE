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
    public class Detail_Discount_Configuration : IEntityTypeConfiguration<DetailDiscount>
    {
        public void Configure(EntityTypeBuilder<DetailDiscount> entity)
        {
            entity.ToTable("Detail_Discount");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.DiscountId).IsRequired();
            entity.Property(e => e.ProductId).IsRequired();

            entity.HasOne(d => d.Discount).WithMany(p => p.DetailDiscounts)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detail_Discount_Discount");

            entity.HasOne(d => d.Product).WithMany(p => p.DetailDiscounts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detail_Discount_Product");
        }
    }
}
