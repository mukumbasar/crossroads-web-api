using Crossroads.Application.Results;
using Crossroads.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Features.AppUser.Commands.AddAppUser
{
    public class AddAppUserCommand : IRequest<DataResult<Domain.Entities.DbSets.AppUser>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Address { get; set; }
    }
}
