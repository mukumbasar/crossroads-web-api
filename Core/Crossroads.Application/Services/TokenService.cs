using Crossroads.Application.Dtos.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Crossroads.Domain.Entities.DbSets;
using Crossroads.Application.Interfaces.Services;

namespace Crossroads.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Dtos.Configurations.TokenOptions _jwtOptions;

        public TokenService(UserManager<IdentityUser> userManager, IOptions<Dtos.Configurations.TokenOptions> options)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<string> GenerateAccessToken(IdentityUser user)
        {
            DateTime notBefore = DateTime.UtcNow;
            DateTime jwtExpiration = notBefore.AddMinutes(_jwtOptions.JWTAccessTokenExpirationTime);

            var securityKey = SignService.GetSymmetricSecurityKey(_jwtOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
           (issuer: _jwtOptions.Issuer,
            notBefore: notBefore, 
            expires: jwtExpiration,
            claims: GetClaims(user, _jwtOptions.Audience).Result,
            signingCredentials: signingCredentials
           );
            
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            return token;
        }

        public async Task<string> GenerateRefreshToken(IdentityUser user)
        {
            var refreshToken = Guid.NewGuid().ToString();
            var hashedRefreshToken = HashRefreshToken(refreshToken);
            var refreshTokenExpirationDate = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshTokenExpirationTime);

            var identityUser = await _userManager.FindByEmailAsync(user.Email);

            // TODO: Make it so refresh token will be added to database using RefreshToken entity. Don't forget to hash it before
            // inserting one into database.

            await _userManager.UpdateAsync(identityUser);

            return hashedRefreshToken;
        }

        public async Task<string> GenerateExpiredAccessToken()
        {
            var securityKey = SignService.GetSymmetricSecurityKey(_jwtOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
            (
                issuer: _jwtOptions.Issuer,
                expires: DateTime.UtcNow.AddMinutes(-1),
                signingCredentials: signingCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            return token;
        }
        private async Task<IEnumerable<Claim>> GetClaims(IdentityUser user, List<string> audiences)
        {    
            var userRoles = await _userManager.GetRolesAsync(user);

            var userList = new List<Claim>
        { 
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            userList.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            return userList;
        }

        private string HashRefreshToken(string refreshToken)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
            return Convert.ToBase64String(hashBytes);
        }
    }
}
