using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zains.Ostad.Application.Users;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.Admin.Users.Commands.ChangeUserRoles
{
    public class ChangeUserRolesCommandHandler : IRequestHandler<ChangeUserRolesCommand, Response>
    {
        private readonly IAppUserManager _appUserManager;
        private readonly IRepository<Role, long> _roleRepository;

        public ChangeUserRolesCommandHandler(IAppUserManager appUserManager, IRepository<Role, long> roleRepository)
        {
            _appUserManager = appUserManager;
            _roleRepository = roleRepository;
        }

        public async Task<Response> Handle(ChangeUserRolesCommand request, CancellationToken cancellationToken)
        {
            var user = await _appUserManager.FindByIdAsync(request.UserId.ToString());
          await _appUserManager.RemoveFromRolesAsync(user,
                await _appUserManager.GetRolesAsync(user));
           await _appUserManager.AddToRolesAsync(user, request.Roles);
            return Response.Success();
        }
    }
}