using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.Users
{
    public class AppUserManager : UserManager<User>, IAppUserManager
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAppRoleManager _roleManager;


        public AppUserManager(IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger,
            IConfiguration configuration,
            IMapper mapper, IAppRoleManager roleManager) : base(store,
            optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,
            logger)
        {
            _configuration = configuration;
            _mapper = mapper;
            _roleManager = roleManager;
        }


        public async Task<object> GenerateJwtToken(string userName, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            await AddUserRolesToJwtClaims(user, claims);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["jwt:JwtExpireDays"]));
            var token = new JwtSecurityToken(
                _configuration["jwt:JwtIssuer"],
                _configuration["jwt:JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task AddUserRolesToJwtClaims(User user, List<Claim> claims)
        {
            var userRoles = await GetRolesAsync(user);
            userRoles.ToList().ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x)));
        }

        public async Task<(IdentityResult res, object token)> GenerateJwtToken(string studentNo)
        {
         
            var user = await Users.FirstOrDefaultAsync(x =>
                x.UserName == studentNo);
            return user is null
                ? (IdentityResult.Failed(), null)
                : (IdentityResult.Success, await GenerateJwtToken(user.UserName, user));
        }
    }
}