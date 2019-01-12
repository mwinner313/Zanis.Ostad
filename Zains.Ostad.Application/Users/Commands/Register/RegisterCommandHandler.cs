using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.Users.Commands.Register
{
    public class RegisterCommandHandler:IRequestHandler<RegisterCommand,LoginRegisterResponse>
    {
        private readonly IAppUserManager _userManager;
        private readonly IMapper _mapper;
        public RegisterCommandHandler(IAppUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<LoginRegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if(_userManager.Users.Any(x=>x.UserName==request.StudentNo))
                return UserAlreadyExists();
            if(_userManager.Users.Any(x=>x.Email==request.EmailAddress))
                return EmailAlreadyExists();
            var user = CreateUserModel(request);
            await _userManager.CreateAsync(user,request.Password);
            return await SuccessWithJwtToken(user);
        }

        private LoginRegisterResponse EmailAlreadyExists()
        {
            return new LoginRegisterResponse()
            {
                Status = ResponseStatus.Fail,
                Message = "ایمیل وارد شده در سیستم موجود میباشد"
            };
        }

        private async Task<LoginRegisterResponse> SuccessWithJwtToken(User user)
        {
            return new LoginRegisterResponse()
            {
                User = _mapper.Map<UserDto>(user),
                BearerToken ="Bearer "+ await _userManager.GenerateJwtToken(user.UserName,user),
                Status = ResponseStatus.Success,
                Message = "ثبت نام شما با موفقیت انجام شد"
            };
        }

        private static User CreateUserModel(RegisterCommand request)
        {
            return new User()
            {
                UserName = request.StudentNo,
                Email = request.EmailAddress,
                FullName = request.FullName,
            };
        }

        private static LoginRegisterResponse UserAlreadyExists()
        {
            return new LoginRegisterResponse()
            {
                Status = ResponseStatus.Fail,
                Message = "شماره دانشجویی وارد شده در سیستم موجود میباشد"
            };
        }
    }
}