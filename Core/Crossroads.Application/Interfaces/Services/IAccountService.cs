using Crossroads.Application.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Interfaces.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// Creates a user account in the Identity-based 'AppUser' table.
        /// </summary>
        /// <param name="email">The email intended to be assigned to the user.</param>
        /// <param name="password">The password intended to be assigned to the user.</param>
        /// <param name="roleName">The role name intended to be assigned to the user.</param>
        /// <returns>Returns the process status.</returns>
        Task<IResult> CreateAsync(string email, string password, string roleName);

        /// <summary>
        /// Deletes a user account from the Identity-based 'AppUser' table.
        /// </summary>
        /// <param name="userId">The ID of the user intended to be deleted.</param>
        /// <returns>Returns the process status.</returns>
        Task<IResult> DeleteAsync(string userId);

        /// <summary>
        /// Updates a user account in the Identity-based 'AppUser' table.
        /// </summary>
        /// <param name="user">The user object intended to be updated.</param>
        /// <returns>Returns the process status.</returns>
        Task<IResult> UpdateAsync(IdentityUser user);

        /// <summary>
        /// Lists all accounts from the Identity-based 'AppUser' table.
        /// </summary>
        /// <returns>Returns users along with the process status.</returns>
        Task<IResult> GetAllAsync();

        /// <summary>
        /// Gets an account by ID from the Identity-based 'AppUser' table.
        /// </summary>
        /// <param name="userId">The ID of the user intended to be retrieved.</param>
        /// <returns>Returns the user with the process status.</returns>
        Task<IResult> GetByIdAsync(string userId);
    }
}
