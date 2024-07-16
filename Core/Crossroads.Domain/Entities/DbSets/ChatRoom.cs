using Crossroads.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.Entities.DbSets
{
    public class ChatRoom : AuditableEntity
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public List<ChatRoomCategory> ChatRoomCategories {  get; set; }
        public List<ChatRoomAdmin> ChatRoomAdmins { get; set; } 
        public List<ChatRoomChatter> ChatRoomChatters { get; set; }
        public List<ChatRoomMessage> ChatRoomMessages { get; set; }
    }
}
