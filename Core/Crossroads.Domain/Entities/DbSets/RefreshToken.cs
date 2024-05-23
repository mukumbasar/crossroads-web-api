﻿using Crossroads.Domain.Entities.Bases;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.Entities.DbSets
{
    public class RefreshToken : AuditableEntity
    {
        public string? Token { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
