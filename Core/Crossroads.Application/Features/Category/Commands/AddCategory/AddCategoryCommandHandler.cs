using AutoMapper;
using Crossroads.Application.Constants;
using Crossroads.Application.Features.AppUser.Commands.AddAppUser;
using Crossroads.Application.Interfaces.Repositories;
using Crossroads.Application.Interfaces.Services;
using Crossroads.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CategoryEntity = Crossroads.Domain.Entities.DbSets.Category;

namespace Crossroads.Application.Features.Category.Commands.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Result>
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly IRedisService _redisService;
        private readonly string keyPrefix = "category";
        public AddCategoryCommandHandler(IMapper mapper,
            IUow uow,
            IRedisService redisService)
        {
            _mapper = mapper;
            _uow = uow;
            _redisService = redisService;
        }

        public async Task<Result> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryRepository = await _uow.GetCategoryRepositoryAsync();
            //ToDo: Check for uniqueness
            var category = _mapper.Map<CategoryEntity>(request);

            var result = await categoryRepository.AddAsync(category);

            if(result != null)
            {
                var redisResult = await _redisService.AddDataAsync($"{keyPrefix}:{category.Id}", JsonConvert.SerializeObject(category));

                return redisResult
                    ? new SuccessDataResult<CategoryEntity>(result, Messages.CategoryCreationSuccess)
                    : new ErrorResult(Messages.CategoryCreationFail);
            }

            return new ErrorResult(Messages.CategoryCreationFail);
        }
    }
}
