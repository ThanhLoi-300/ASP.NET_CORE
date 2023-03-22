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
    public class Discount_Configuration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> entity)
        {
            entity.ToTable("Discount");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.EndTime).IsRequired();
            entity.Property(e => e.IsDeleted).IsRequired();
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.Status)
                .HasMaxLength(100).IsRequired();
            entity.Property(e => e.Value).IsRequired();
        }
    }
}
