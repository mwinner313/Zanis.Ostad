using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.Admin.Users.Queries
{
    public class GetRolesListQueryHandler:IRequestHandler<GetRolesListQuery,List<RoleViewModel>>
    {
        private readonly IRepository<Role, long> _repository;

        public GetRolesListQueryHandler(IRepository<Role, long> repository)
        {
            _repository = repository;
        }

        public Task<List<RoleViewModel>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetQueryable().Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync(cancellationToken);
        }
    }
}