using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Zains.Ostad.Application.Users;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.AutoMapperProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ForMember(x=>x.Roles,opt=>opt.MapFrom<UserRoleResolver>());
        }
    }

    public class UserRoleResolver : IValueResolver<User, UserDto, List<string>>
    {
        private readonly IAppUserManager _userManager;

        public UserRoleResolver(IAppUserManager userManager)
        {
            _userManager = userManager;
        }

        public List<string> Resolve(User source, UserDto destination, List<string> destMember, ResolutionContext context)
        {
            return _userManager.GetRolesAsync(source).Result.ToList();
        }
    }
}