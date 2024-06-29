using AutoMapper;
using Crossroads.Application.Constants;
using Crossroads.Application.Dtos.Queries;
using Crossroads.Application.Features.AppUser.Commands.AddAppUser;
using Crossroads.Application.Interfaces.Repositories;
using Crossroads.Application.Interfaces.Services;
using Crossroads.Application.Results;
using MediatR;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CategoryEntity = Crossroads.Domain.Entities.DbSets.Category;

namespace Crossroads.Application.Features.Category.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly IRedisService _redisService;
        private readonly string keyPrefix = "category";

        public GetAllCategoriesQueryHandler(IMapper mapper, 
            IUow uow,
            IRedisService redisService)
        {
            _mapper = mapper;
            _uow = uow;
            _redisService = redisService;
        }

        public async Task<Result> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {

            var categoryDictionary = await _redisService.GetDataAsync(keyPrefix);

            if (categoryDictionary.Any())
            {
                var categories = categoryDictionary.Values
                    .Select(JsonConvert.DeserializeObject<CategoryEntity>)
                    .ToList();

                var categoryDtos = _mapper.Map<List<CategoryListDto>>(categories);
                return new SuccessDataResult<List<CategoryListDto>>(categoryDtos, Messages.CategoryListingSuccess);
            }

            return new ErrorResult(Messages.CategoryListingFail);
        }
    }
}
