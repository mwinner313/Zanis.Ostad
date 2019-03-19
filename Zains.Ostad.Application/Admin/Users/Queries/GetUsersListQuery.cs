using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Users.Dto;

namespace Zains.Ostad.Application.Admin.Users.Queries
{
    public class GetUsersListQuery:Pagenation,IRequest<PagenatedList<UserDto>>
    {
        public long? RoleId { get; set; }
        public string Search { get; set; }
    }
}