using AutoMapper;
using Crossroads.Application.Dtos.Queries;
using Crossroads.Application.Features.AppUser.Commands.AddAppUser;
using Crossroads.Application.Features.Category.Commands.AddCategory;
using Crossroads.Domain.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<AddCategoryCommand, Category>();
            CreateMap<Category, CategoryListDto>();
        }
    }
}
