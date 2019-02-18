using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Zains.Ostad.Application.Users.Commands.Login;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Users.Commands.ConfirmChangePassword
{
    public class
        ConfirmChangePasswordCommandHandler : IRequestHandler<ConfirmChangePasswordCommand, LoginRegisterResponse>
    {
        private readonly IAppUserManager _userManager;
        private readonly IMediator _mediator;

        public ConfirmChangePasswordCommandHandler(IAppUserManager userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<LoginRegisterResponse> Handle(ConfirmChangePasswordCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
                return new LoginRegisterResponse {Status = ResponseStatus.Fail, Message = "user not found"};
            var res = await _userManager.ResetPasswordAsync(user, request.Token.Replace(' ', '+'), request.NewPassword);
            return res.Succeeded
                ? await _mediator.Send(new LoginCommand()
                {
                    UserName = user.UserName,
                    Password = request.NewPassword
                }, cancellationToken)
                : new LoginRegisterResponse
                    {Status = ResponseStatus.Fail, Message = string.Join(',', res.Errors.Select(x => x.Description))};
        }
    }
}