using System.Linq;
using Microsoft.AspNetCore.Identity;
using Zanis.Ostad.Core.Contracts;

namespace Zanis.Ostad.Repository
{
    public class EfUserRoleRepository:IUserRoleRepository
    {
        private readonly ApplicationDbContext _applicationDb;

        public EfUserRoleRepository(ApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }

        public IQueryable<IdentityUserRole<long>> GetQueryable()
        {
            return _applicationDb.UserRoles;
        }
    }
}