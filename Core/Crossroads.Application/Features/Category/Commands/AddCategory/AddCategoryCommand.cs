using Crossroads.Application.Results;
using Crossroads.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Features.Category.Commands.AddCategory
{
    public class AddCategoryCommand : IRequest<Result>
    {
        public string Name { get; set; }
    }
}
