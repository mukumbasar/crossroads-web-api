using Crossroads.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.Entities.DbSets
{
    public class ChatRoomChatter : AuditableEntity
    {
        public Guid ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; } 
        public List<ChatRoomMessage> ChatRoomMessages { get; set; }
    }
}
