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
    public class ChatRoomChatterConfiguration : AuditableEntityTypeConfiguration<ChatRoomChatter>
    {
        public override void Configure(EntityTypeBuilder<ChatRoomChatter> builder)
        {
            base.Configure(builder);

            builder.HasMany(x => x.ChatRoomMessages).WithOne(crm => crm.ChatRoomChatter).HasForeignKey(crm => crm.ChatRoomChatterId);
        }
    }
}
