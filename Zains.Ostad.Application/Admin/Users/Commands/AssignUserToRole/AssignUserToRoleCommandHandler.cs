using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zains.Ostad.Application.Users;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Admin.Users.Commands.AssignUserToRole
{
    public class AssignUserToRoleCommandHandler : IRequestHandler<AssignUserToRoleCommand, Response>
    {
        private readonly IAppUserManager _appUserManager;

        public AssignUserToRoleCommandHandler(IAppUserManager appUserManager)
        {
            _appUserManager = appUserManager;
        }

        public async Task<Response> Handle(AssignUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _appUserManager.FindByIdAsync(request.UserId.ToString());
            await _appUserManager.AddToRoleAsync(user,
                request.Role);
            return Response.Success();
        }
    }
}