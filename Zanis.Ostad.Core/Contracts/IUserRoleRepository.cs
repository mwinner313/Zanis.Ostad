using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Zanis.Ostad.Core.Contracts
{
    public interface IUserRoleRepository
    {
        IQueryable<IdentityUserRole<long>> GetQueryable();
    }
}