using System.Collections.Generic;
using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Admin.Users.Commands.ChangeUserRoles
{
    public class ChangeUserRolesCommand:IRequest<Response>
    {
        public List<string> Roles { get; set; }
        public long UserId { get; set; }
    }
}