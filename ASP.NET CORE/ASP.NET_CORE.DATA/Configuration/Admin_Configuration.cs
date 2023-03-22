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
    public class Admin_Configuration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> entity)
        {
            entity.ToTable("Admin");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Account)
                .HasMaxLength(255).IsRequired();
            entity.Property(e => e.Email)
                .HasMaxLength(255).IsRequired();
            entity.Property(e => e.Name)
                .HasMaxLength(255).IsRequired();
            entity.Property(e => e.Password)
                .HasMaxLength(255).IsRequired();
            entity.Property(e => e.RoleId).IsRequired();

            entity.HasOne(t => t.Role).WithMany(pc => pc.Admins)
                .HasForeignKey(t => t.RoleId)
                .HasConstraintName("FK_Admin_Role"); ;
        }
    }
}
