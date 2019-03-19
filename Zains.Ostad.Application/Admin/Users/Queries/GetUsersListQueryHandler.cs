using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.Admin.Users.Queries
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, PagenatedList<UserDto>>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(IUserRoleRepository userRoleRepository, IRepository<User, long> userRepository,
            IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagenatedList<UserDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var queriable = _userRepository.GetQueryable();

            if (request.RoleId.HasValue)
            {
                var usersInRoleIds = _userRoleRepository.GetQueryable().Where(x => x.RoleId == request.RoleId)
                    .Select(x => x.UserId).ToList();
                queriable = queriable.Where(x => usersInRoleIds.Contains(x.Id));
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                queriable = queriable.Where(x => x.UserName.Contains(request.Search));
            }

            return new PagenatedList<UserDto>
            {
                AllCount = queriable.Count(),
                Items = _mapper.Map<List<UserDto>>(await queriable.Pagenate(request).ToListAsync())
            };
        }
    }
}