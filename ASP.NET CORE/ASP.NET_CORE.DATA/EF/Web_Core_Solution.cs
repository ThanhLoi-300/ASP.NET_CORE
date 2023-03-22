using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ASP.NET_CORE.DATA.EF
{
    public class Web_Core_Solution : IDesignTimeDbContextFactory<Web_Core_DbContext>
    {
        public Web_Core_DbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var conn = configuration.GetConnectionString("Connection_String");
            var option = new DbContextOptionsBuilder<Web_Core_DbContext>();
            option.UseSqlServer(conn);

            return new Web_Core_DbContext(option.Options);
        }
    }
}
