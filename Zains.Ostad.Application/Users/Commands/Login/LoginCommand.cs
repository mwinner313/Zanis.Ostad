using MediatR;
using Zains.Ostad.Application.Users.Dto;

namespace Zains.Ostad.Application.Users.Commands.Login
{
    public class LoginCommand : IRequest<LoginRegisterResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}