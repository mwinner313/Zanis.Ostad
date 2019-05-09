using System;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.Notifications
{
    public class Notification:BaseEntity<long>
    {
        public NotificationRelatedItemType RelatedItemType { get; set; }
        public bool IsSeen { get; set; }
        public string JsonExtraData { get; set; }
        public string Text { get; set; }
        public User Receiver { get; set; }
        public long ReceiverId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}