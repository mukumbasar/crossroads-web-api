using Crossroads.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.Entities.DbSets
{
    public class ChatRoomMessage : AuditableEntity
    {
        public Guid ChatRoomChatterId { get; set; }
        public ChatRoomChatter ChatRoomChatter { get; set; }
        public Guid ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
        public string SenderName { get; set; }
        public string TextData { get; set; }
    }
}
