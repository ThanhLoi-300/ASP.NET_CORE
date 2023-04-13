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
    public class Client_Configuration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> entity)
        {
            entity.ToTable("client");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Account)
                .HasMaxLength(255).IsRequired();
            entity.Property(e => e.Address)
                .HasMaxLength(500).IsRequired();
            entity.Property(e => e.Email)
                .HasMaxLength(255).IsRequired();
            entity.Property(e => e.Img)
                .HasMaxLength(255);
            entity.Property(e => e.Name)
                .HasMaxLength(255).IsRequired();
            entity.Property(e => e.Password)
                .HasMaxLength(255).IsRequired();
            entity.Property(e => e.Sdt).IsRequired();
            entity.Property(e => e.Sex).IsRequired();
        }
    }
}
