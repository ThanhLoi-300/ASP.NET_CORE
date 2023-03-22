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
    public class Category_Configuration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.ToTable("Category");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.IsDeleted).IsRequired();
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsFixedLength().IsRequired();
            entity.Property(e => e.Status).IsRequired();
        }
    }
}
