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
        /// Id si verilen hesabı siler.
        /// </summary>
        /// <param name="id">Silinecek hesabın id sini temsil eder</param>
        /// <returns>Silme işleminin başarı durumunu döner</returns>
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
        /// Bütün hesapları listeler
        /// </summary>
        /// <returns>Hesapları içeren bir list ve durum mesajını döner</returns>
        public async Task<IResult> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users.Count <= 0) return new ErrorResult(Messages.ListHasNoAccounts);

            return new SuccessDataResult<List<IdentityUser>>(users, Messages.AccountsListedSuccessfully);
        }
        /// <summary>
        /// Id si verilen hesabı getirir
        /// </summary>
        /// <param name="id">İstenilen hesabın id sini temsil eder</param>
        /// <returns>Hesap ve sonuç mesajını döner</returns>
        public async Task<IResult> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return new ErrorResult(Messages.AccountNotFound);

            return new SuccessDataResult<IdentityUser>(user, Messages.AccountFoundSuccessfully);
        }
        /// <summary>
        /// Verilen hesabı günceller
        /// </summary>
        /// <param name="user">Güncellenecek hesabı temsil eder</param>
        /// <returns>Güncellenen hesapla birlikte durum mesajını döner</returns>
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
