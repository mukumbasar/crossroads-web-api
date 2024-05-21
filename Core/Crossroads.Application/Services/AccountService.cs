using AutoMapper;
using Crossroads.Application.Constants;
using Crossroads.Application.Interfaces.Services;
using Crossroads.Application.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailQueueService _emailQueueService;

        public AccountService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IEmailQueueService emailQueueService = null)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailQueueService = emailQueueService;
        }
        /// <summary>
        /// Creates a user account in the Identity-based 'AppUser' table.
        /// </summary>
        /// <param name="email">The email intended to be assigned to the user.</param>
        /// <param name="password">The password intended to be assigned to the user.</param>
        /// <param name="roleName">The role name intended to be assigned to the user.</param>
        /// <returns>Returns the process status.</returns>
        public async Task<IResult> CreateAsync(string email, string password, string roleName)
        {
            var hasAccount = await _userManager.FindByEmailAsync(email);
            if (hasAccount != null) return new ErrorResult(Messages.AccountAlreadyExists);

            IdentityUser user = new IdentityUser()
            {
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) return new ErrorResult(Messages.AccountCreationFailed);

            var role = await _roleManager.FindByNameAsync(roleName);
            var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!roleResult.Succeeded) return new ErrorResult(Messages.RoleAssignmentFailed);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var body = GenerateActivationLink(token);

            await _emailQueueService.QueueMessageAsync(email, "Account Activation", body);

            return new SuccessDataResult<IdentityUser>(user, Messages.AccountCreationSucceeded);
        }

        /// <summary>
        /// Deletes a user account from the Identity-based 'AppUser' table.
        /// </summary>
        /// <param name="id">The ID of the user intended to be deleted.</param>
        /// <returns>Returns the process status.</returns>
        public async Task<IResult> DeleteAsync(string id)
        {
            var account = await _userManager.FindByIdAsync(id);

            if (account != null)
            {
                await _userManager.DeleteAsync(account);
                return new SuccessResult(Messages.AccountDeletionSucceeded);
            }

            return new ErrorResult(Messages.AccountNotFound);
        }
        /// <summary>
        /// Lists all accounts from the Identity-based 'AppUser' table.
        /// </summary>
        /// <returns>Returns users along with the process status.</returns>
        public async Task<IResult> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users.Count <= 0) return new ErrorResult(Messages.ListHasNoAccounts);

            return new SuccessDataResult<List<IdentityUser>>(users, Messages.AccountsListedSuccessfully);
        }
        /// <summary>
        /// Gets an account by ID from the Identity-based 'AppUser' table.
        /// </summary>
        /// <param name="id">The ID of the user intended to be retrieved.</param>
        /// <returns>Returns the user with the process status.</returns>
        public async Task<IResult> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return new ErrorResult(Messages.AccountNotFound);

            return new SuccessDataResult<IdentityUser>(user, Messages.AccountFoundSuccessfully);
        }
        /// <summary>
        /// Updates a user account in the Identity-based 'AppUser' table.
        /// </summary>
        /// <param name="user">The user object intended to be updated.</param>
        /// <returns>Returns the process status.</returns>
        public async Task<IResult> UpdateAsync(IdentityUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return new ErrorResult(Messages.AccountUpdateFailed);

            return new SuccessDataResult<IdentityUser>(user, Messages.AccountUpdateSucceeded);
        }

        private string GenerateActivationLink(string token)
        {
            // Construct the activation link
            return $"https://localhost:5000/activation/{token}";
        }
    }
}
