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
    public class ChatRoomCategoryConfiguration : AuditableEntityTypeConfiguration<ChatRoomCategory>
    {
        public override void Configure(EntityTypeBuilder<ChatRoomCategory> builder)
        {
            base.Configure(builder);
        }
    }
}
