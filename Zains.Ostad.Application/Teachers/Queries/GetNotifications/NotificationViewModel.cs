using System;
using Zanis.Ostad.Core.Entities.Notifications;

namespace Zains.Ostad.Application.Teachers.Queries.GetNotifications
{
    public class NotificationViewModel
    {
        public NotificationRelatedItemType RelatedItemType { get; set; }
        public bool IsSeen { get; set; }
        public string JsonExtraData { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}