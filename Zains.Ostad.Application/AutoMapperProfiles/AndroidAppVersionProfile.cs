using AutoMapper;
using Zains.Ostad.Application.AndroidAppVersions.Dtos;
using Zanis.Ostad.Core.Entities.AndroidApp;

namespace Zains.Ostad.Application.AutoMapperProfiles
{
    public class AndroidAppVersionProfile:Profile
    {
        public AndroidAppVersionProfile()
        {
            CreateMap<AppVersion, AppVersionViewModel>();
        }
    }
}