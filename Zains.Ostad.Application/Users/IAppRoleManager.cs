using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.Users
{
    public interface IAppRoleManager
    {
        Task<IdentityResult> CreateAsync(Role role);
        Task UpdateNormalizedRoleNameAsync(Role role);
        Task<IdentityResult> UpdateAsync(Role role);
        Task<IdentityResult> DeleteAsync(Role role);
        Task<bool> RoleExistsAsync(string roleName);
        string NormalizeKey(string key);
        Task<Role> FindByIdAsync(string roleId);
        Task<string> GetRoleNameAsync(Role role);
        Task<IdentityResult> SetRoleNameAsync(Role role, string name);
        Task<string> GetRoleIdAsync(Role role);
        Task<Role> FindByNameAsync(string roleName);
        Task<IdentityResult> AddClaimAsync(Role role, Claim claim);
        Task<IdentityResult> RemoveClaimAsync(Role role, Claim claim);
        Task<IList<Claim>> GetClaimsAsync(Role role);
        void Dispose();
        ILogger Logger { get; set; }
        IList<IRoleValidator<Role>> RoleValidators { get; }
        IdentityErrorDescriber ErrorDescriber { get; set; }
        ILookupNormalizer KeyNormalizer { get; set; }
        IQueryable<Role> Roles { get; }
        bool SupportsQueryableRoles { get; }
        bool SupportsRoleClaims { get; }
    }
}