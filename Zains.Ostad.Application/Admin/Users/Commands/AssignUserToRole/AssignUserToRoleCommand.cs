using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Admin.Users.Commands.AssignUserToRole
{
    public class AssignUserToRoleCommand:IRequest<Response>
    {
        public string Role { get; set; }
        public long UserId { get; set; }
    }
}