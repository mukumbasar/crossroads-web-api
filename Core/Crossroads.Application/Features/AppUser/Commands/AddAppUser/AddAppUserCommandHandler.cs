using AutoMapper;
using Crossroads.Application.Constants;
using Crossroads.Application.Interfaces.Repositories;
using Crossroads.Application.Interfaces.Services;
using Crossroads.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppUserEntity = Crossroads.Domain.Entities.DbSets.AppUser;

namespace Crossroads.Application.Features.AppUser.Commands.AddAppUser
{
    public class AddAppUserCommandHandler : IRequestHandler<AddAppUserCommand, Result>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly IImageConversionService _imageConversionService;
        public AddAppUserCommandHandler(UserManager<IdentityUser> userManager,
            IMapper mapper,
            IUow uow,
            IAccountService accountService,
            IImageConversionService imageConversionService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _uow = uow;
            _accountService = accountService;
            _imageConversionService = imageConversionService;
        }

        public async Task<Result> Handle(AddAppUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                return new ErrorResult(Messages.AccountAlreadyExists);
            }

            var appUserRepository = await _uow.GetAppUserRepositoryAsync();

            Result result = new ErrorResult(Messages.AccountCreationFailed);

            IdentityUser newUser = new IdentityUser()
            {
                Email = request.Email,
                UserName = request.Email,
                EmailConfirmed = true
            };

            var strategy = await appUserRepository.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await appUserRepository.BeginTransactionAsync();

                try
                {
                    var accountResult = await _accountService.CreateAsync(request.Email, request.Password, "AppUser");
                    
                    if (!accountResult.IsSuccess)
                    {
                        result = new ErrorResult(accountResult.Message);
                        transaction.Rollback();
                        return;
                    }

                    var newAppUser = _mapper.Map<AppUserEntity>(request);
                    
                    newAppUser.IdentityId = newUser.Id;
                    if(request.ImageFile != null) newAppUser.Image = await _imageConversionService.ConvertToByteArrayAsync(request.ImageFile);

                    await appUserRepository.AddAsync(newAppUser);
                    await _uow.CommitAsync();

                    result = new SuccessResult(Messages.AccountCreationSucceeded);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    result = new ErrorResult(Messages.AccountCreationFailed);
                    transaction.Rollback();
                }
                finally
                {
                    transaction.Dispose();
                }
            });

            return result;
        }
    }
}
