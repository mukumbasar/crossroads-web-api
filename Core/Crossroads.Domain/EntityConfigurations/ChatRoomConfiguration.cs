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
    public class ChatRoomConfiguration : AuditableEntityTypeConfiguration<ChatRoom>
    {
        public override void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(70);
            builder.Property(x => x.Image).IsRequired();

            builder.HasMany(x => x.ChatRoomCategories).WithOne(crc => crc.ChatRoom).HasForeignKey(crc => crc.ChatRoomId);
            builder.HasMany(x => x.ChatRoomAdmins).WithOne(cra => cra.ChatRoom).HasForeignKey(cra => cra.ChatRoomId);
            builder.HasMany(x => x.ChatRoomChatters).WithOne(crc => crc.ChatRoom).HasForeignKey(crc => crc.ChatRoomId);
            builder.HasMany(x => x.ChatRoomMessages).WithOne(crm => crm.ChatRoom).HasForeignKey(crm => crm.ChatRoomId);
        }
    }
}
