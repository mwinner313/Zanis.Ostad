using System.Collections.Generic;
using MediatR;

namespace Zains.Ostad.Application.Admin.Users.Queries
{
    public class GetRolesListQuery:IRequest<List<RoleViewModel>>
    {
    }
}