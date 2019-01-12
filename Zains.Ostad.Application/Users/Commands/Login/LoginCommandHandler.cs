using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginRegisterResponse>
    {
        private readonly IAppUserManager _userManager;
        private readonly IMapper _mapper;
        public LoginCommandHandler(IAppUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<LoginRegisterResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return Failed();
            return await Success(user);
        }

        private async Task<LoginRegisterResponse> Success(User user)
        {
            return new LoginRegisterResponse
            {
                Status = ResponseStatus.Success,
                BearerToken = "Bearer " + await _userManager.GenerateJwtToken(user.UserName, user),
                User = _mapper.Map<UserDto>(user)
            };
        }

        private LoginRegisterResponse Failed()
        {
            return new LoginRegisterResponse
            {
                Status = ResponseStatus.Fail,
                Message = "رمز عبور یا نام کاربری اشتباه است"
            };
        }
    }
}