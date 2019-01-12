using MediatR;
using Zains.Ostad.Application.Users.Dto;

namespace Zains.Ostad.Application.Users.Commands.Register
{
    public class RegisterCommand : IRequest<LoginRegisterResponse>
    {
        public string StudentNo { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}