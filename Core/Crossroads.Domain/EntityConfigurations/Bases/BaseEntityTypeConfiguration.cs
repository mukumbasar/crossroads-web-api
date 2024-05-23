using Crossroads.Domain.Entities.Bases;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.EntityConfigurations.Base
{
    public class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedBy).HasMaxLength(128).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.Property(x => x.ModifiedBy).HasMaxLength(128).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
        }
    }
}
