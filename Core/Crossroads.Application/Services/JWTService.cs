using Crossroads.Application.Dtos.Configurations;
using Crossroads.Application.Interfaces;
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

namespace Crossroads.Application.Services
{
    public class JWTService : IJWTService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWTOption _jwtOption;

        public JWTService(UserManager<IdentityUser> userManager, IOptions<JWTOption> options)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtOption = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<string> GenerateAccessToken(IdentityUser user)
        {
            DateTime notBefore = DateTime.UtcNow;
            DateTime jwtExpiration = notBefore.AddMinutes(_jwtOption.JWTExpiration);

            var securityKey = SignService.GetSymmetricSecurityKey(_jwtOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
           (issuer: _jwtOption.Issuer,
            notBefore: notBefore, 
            expires: jwtExpiration,
            claims: GetClaims(user, _jwtOption.Audience).Result,
            signingCredentials: signingCredentials
           );
            
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            return token;
        }

        public async Task<string> GenerateExpiredAccessToken()
        {
            var securityKey = SignService.GetSymmetricSecurityKey(_jwtOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
            (
                issuer: _jwtOption.Issuer,
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
    }
}
