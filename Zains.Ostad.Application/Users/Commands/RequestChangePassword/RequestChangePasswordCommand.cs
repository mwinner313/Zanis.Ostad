using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Users.Commands.RequestChangePassword
{
    public class RequestChangePasswordCommand:IRequest<Response>
    {
        public string UserName { get; set; }
    }
}