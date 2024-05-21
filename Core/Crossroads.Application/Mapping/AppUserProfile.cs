using AutoMapper;
using Crossroads.Application.Features.AppUser.Commands.AddAppUser;
using Crossroads.Domain.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Mapping
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AddAppUserCommand, AppUser>();
        }
    }
}
