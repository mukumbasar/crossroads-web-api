using Crossroads.Application.Interfaces.Repositories;
using Crossroads.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Persistence.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly CrossroadsDbContext _context;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ICategoryRepository _categoryRepository;

        public Uow(CrossroadsDbContext context,
            IAppUserRepository appUserRepository,
            IRefreshTokenRepository refreshTokenRepository,
            ICategoryRepository categoryRepository) 
        {
            _context = context;
            _appUserRepository = appUserRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IAppUserRepository> GetAppUserRepositoryAsync()
        {
            return await Task.FromResult(_appUserRepository);
        }


        public async Task<IRefreshTokenRepository> GetRefreshTokenRepositoryAsync()
        {
            return await Task.FromResult(_refreshTokenRepository);
        }

        public async Task<ICategoryRepository> GetCategoryRepositoryAsync()
        {
            return await Task.FromResult(_categoryRepository);
        }
    }
}
