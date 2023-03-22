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
    public class Role_Configuration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable("Role");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description)
                .HasMaxLength(1000).IsRequired();
            entity.Property(e => e.Name)
                .HasMaxLength(255).IsRequired();
        }
    }
}
