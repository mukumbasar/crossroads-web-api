using Crossroads.Domain.Entities.DbSets;
using Crossroads.Domain.Entities.Interfaces;
using Crossroads.Domain.EntityConfigurations.Base;
using Crossroads.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.EntityConfigurations
{
    public class AppUserConfiguration : AuditableEntityTypeConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.Image).IsRequired(false);
            builder.Property(x => x.Address).IsRequired(false);
        }
    }
}
