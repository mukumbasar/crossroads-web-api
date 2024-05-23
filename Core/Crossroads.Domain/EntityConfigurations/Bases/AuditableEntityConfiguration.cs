using Crossroads.Domain.Entities.Bases;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.EntityConfigurations.Base
{
    public class AuditableEntityTypeConfiguration<TEntity> : BaseEntityTypeConfiguration<TEntity> where TEntity : AuditableEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.DeletedBy).HasMaxLength(128).IsRequired(false);
            builder.Property(x => x.DeletedDate).IsRequired(false);
        }
    }
}
