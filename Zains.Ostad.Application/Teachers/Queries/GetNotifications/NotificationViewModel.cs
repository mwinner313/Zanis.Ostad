using System;
using Newtonsoft.Json.Linq;
using Zanis.Ostad.Core.Entities.Notifications;

namespace Zains.Ostad.Application.Teachers.Queries.GetNotifications
{
    public class NotificationViewModel
    {
        public long Id { get; set; }
        public NotificationRelatedItemType RelatedItemType { get; set; }
        public bool IsSeen { get; set; }
        public JObject JsonExtraData { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}