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
    public class Img_Configuration : IEntityTypeConfiguration<Img>
    {
        public void Configure(EntityTypeBuilder<Img> entity)
        {
            entity.ToTable("Img");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.ImgProduct).IsRequired();
            entity.Property(e => e.SubImg).IsRequired();

            entity.HasOne(d => d.Product).WithMany(p => p.Imgs)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Img_Product");
        }
    }
}
