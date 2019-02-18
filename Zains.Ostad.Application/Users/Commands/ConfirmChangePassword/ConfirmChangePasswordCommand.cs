using MediatR;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Users.Commands.ConfirmChangePassword
{
    public class ConfirmChangePasswordCommand:IRequest<LoginRegisterResponse>
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
    }
}