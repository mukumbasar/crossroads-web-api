using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Persistence.Context
{
    public class CrossroadsDbContextFactory : IDesignTimeDbContextFactory<CrossroadsDbContext>
    {
        //Design-time configurations
        public CrossroadsDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<CrossroadsDbContext>();
            var connectionString = configuration.GetConnectionString("MainConnection");

            builder.UseSqlServer(connectionString);

            return new CrossroadsDbContext(builder.Options);
        }
    }
}
