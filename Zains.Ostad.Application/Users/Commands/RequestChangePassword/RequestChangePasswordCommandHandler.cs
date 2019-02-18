using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Users.Commands.RequestChangePassword
{
    public class RequestChangePasswordCommandHandler:IRequestHandler<RequestChangePasswordCommand,Response>
    {
        private readonly IAppUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        public RequestChangePasswordCommandHandler(IAppUserManager userManager, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response> Handle(RequestChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user is null) return Response.Failed("چنین کاربری در سیستم وجود ندارد");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _emailService.SendAsync(new EmailMessage
            {
                Body = $"<a href='{GetDomainUrl()}/changepassword?token={token}'>برای تغییر رمز عبور خود کلیک کنید</a> ",
                Destination = user.Email,
                Subject = "تغییر رمز عبور - استاد زانیس"
            });
            return Response.Success();
        }
        private string GetDomainUrl()
        {
            var str = _httpContextAccessor.HttpContext.Request.Host.Value;
            if (!str.StartsWith(_httpContextAccessor.HttpContext.Request.Scheme))
                return _httpContextAccessor.HttpContext.Request.Scheme + "://" + str;
            return str;
        }
    }
}