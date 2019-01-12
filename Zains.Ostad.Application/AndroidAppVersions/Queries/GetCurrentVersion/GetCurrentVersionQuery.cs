using MediatR;
using Zains.Ostad.Application.AndroidAppVersions.Dtos;

namespace Zains.Ostad.Application.AndroidAppVersions.Queries.GetCurrentVersion
{
    public class GetCurrentVersionQuery:IRequest<AppVersionViewModel>
    {
    }
}