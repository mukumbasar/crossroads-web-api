using Crossroads.Domain.Entities.DbSets;
using Crossroads.Domain.EntityConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.EntityConfigurations
{
    public class CategoryConfiguration : AuditableEntityTypeConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(40);
            builder.HasMany(x => x.ChatRoomCategories).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
        }
    }
}
