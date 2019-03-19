using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Users;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Contracts;

namespace Zains.Ostad.Application.Admin.Edits.Queries.GetEditorsList
{
    public class GetEditorsListQueryHandler : IRequestHandler<GetEditorsListQuery, PagenatedList<UserDto>>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IAppRoleManager _appRoleManager;
        private readonly IAppUserManager _appUserManager;
        private readonly IMapper _mapper;
        public GetEditorsListQueryHandler(IUserRoleRepository userRoleRepository, IAppRoleManager appRoleManager,
            IAppUserManager appUserManager, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _appRoleManager = appRoleManager;
            _appUserManager = appUserManager;
            _mapper = mapper;
        }

        public async Task<PagenatedList<UserDto>> Handle(GetEditorsListQuery request, CancellationToken cancellationToken)
        {
            var role = await _appRoleManager.FindByNameAsync("Editor");
            var userIds =  _userRoleRepository.GetQueryable().Where(x => x.RoleId == role.Id).Select(x => x.UserId);
            var users = _appUserManager.Users.Where(x => userIds.Contains(x.Id));
            if (!string.IsNullOrEmpty(request.Search))
            {
                users = users.Where(x => x.UserName.Contains(request.Search) || x.FullName.Contains(request.Search));
            }
            return new PagenatedList<UserDto>
            {
                AllCount = users.Count(),
                Items = _mapper.Map<List<UserDto>>(users.OrderBy(x=>x.Id).Pagenate(request).ToList())
            };
        }
    }
}