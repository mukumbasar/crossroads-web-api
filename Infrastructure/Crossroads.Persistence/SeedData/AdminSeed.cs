using Crossroads.Domain.Entities.DbSets;
using Crossroads.Domain.Enums;
using Crossroads.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Persistence.SeedData
{
    internal static class AdminSeed
    {
        private const string AdminEmail = "admin33333@crossroads.com";
        private const string AdminPassword = "Admin-3";

        public static async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<CrossroadsDbContext>();

            dbContextBuilder.UseSqlServer(configuration.GetConnectionString("MainConnection"));

            using CrossroadsDbContext context = new(dbContextBuilder.Options);

            if (!context.Roles.Any())
            {
                await AddRoles(context);
            }

            if (!context.Users.Any(user => user.Email == AdminEmail))
            {
                await AddAdmin(context);
            }

            await Task.CompletedTask;
        }
  
        private static async Task AddAdmin(CrossroadsDbContext context)
        {
            IdentityUser user = new()
            {
                UserName = AdminEmail,
                NormalizedUserName = AdminEmail.ToUpper(),
                Email = AdminEmail,
                NormalizedEmail = AdminEmail.ToUpper(),
                EmailConfirmed = true
            };
            user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, AdminPassword);
            var identityUser = await context.Users.AddAsync(user);

            var adminRoleId = context.Roles.FirstOrDefault(role => role.Name == Roles.Admin.ToString())!.Id;

            await context.UserRoles.AddAsync(new IdentityUserRole<string> { UserId = user.Id, RoleId = adminRoleId });
            
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "default-image.jpg");
            byte[] defaultImageBytes = await File.ReadAllBytesAsync(filePath);

            AppUser appUser = new()
            {
                FirstName = "Admin",
                LastName = "Admin",
                Address = "Address",
                Gender = Gender.Male,
                IdentityId = identityUser.Entity.Id,
                Image = defaultImageBytes
            };

            await context.AppUsers.AddAsync(appUser);

            await context.SaveChangesAsync();
        }

        private static async Task AddRoles(CrossroadsDbContext context)
        {
            string[] roles = Enum.GetNames(typeof(Roles));
            for (int i = 0; i < roles.Length; i++)
            {
                if (await context.Roles.AnyAsync(role => role.Name == roles[i]))
                {
                    continue;
                }

                await context.Roles.AddAsync(new IdentityRole { Name = roles[i], NormalizedName = roles[i].ToUpperInvariant() });
            }

            await context.SaveChangesAsync();
        }
    }
}
