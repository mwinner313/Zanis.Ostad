using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Users;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.Edits.Queries.GetEditorsList
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
            var role = _appRoleManager.FindByNameAsync("Editor");
            var userIds = _userRoleRepository.GetQueryable().Where(x => x.RoleId == role.Id).Select(x => x.UserId);
            var users = _appUserManager.Users.Where(x => userIds.Contains(x.Id));
            if (!string.IsNullOrEmpty(request.Search))
            {
                users = users.Where(x => x.UserName.Contains(request.Search) || x.FullName.Contains(request.Search));
            }
            return new PagenatedList<UserDto>
            {
                AllCount = users.Count(),
                Items = users.ProjectTo<UserDto>().Pagenate(request).ToList()
            };
        }
    }
}