using Crossroads.Domain.Entities.Bases;
using Crossroads.Domain.EntityConfigurations.Interfaces;
using Crossroads.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Persistence.Context
{
    public class CrossroadsDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public CrossroadsDbContext(DbContextOptions<CrossroadsDbContext> options) : base(options)
        {
            
        }

        //DbSets:

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityTypeConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
