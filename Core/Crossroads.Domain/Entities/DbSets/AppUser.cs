using Crossroads.Domain.Entities.Bases;
using Crossroads.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.Entities.DbSets
{
    public class AppUser : BaseUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] Image { get; set; }
        public string? Address { get; set; }
        public List<ChatRoomAdmin> ChatRoomAdmins { get; set; }
        public List<ChatRoomChatter> ChatRoomChatters { get; set; }
    }
}
