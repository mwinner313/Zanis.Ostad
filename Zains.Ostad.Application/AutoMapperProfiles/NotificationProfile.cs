using AutoMapper;
using Newtonsoft.Json.Linq;
using Zains.Ostad.Application.Teachers.Queries.GetNotifications;
using Zanis.Ostad.Core.Entities.Notifications;

namespace Zains.Ostad.Application.AutoMapperProfiles
{
    public class NotificationProfile:Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationViewModel>().ForMember(x => x.JsonExtraData,
                opt => opt.MapFrom(x => JObject.Parse(x.JsonExtraData)));
        }
    }
}