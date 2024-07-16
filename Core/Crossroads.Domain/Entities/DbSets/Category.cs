using Crossroads.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.Entities.DbSets
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; }
        public List<ChatRoomCategory> ChatRoomCategories { get; set; }
    }
}
