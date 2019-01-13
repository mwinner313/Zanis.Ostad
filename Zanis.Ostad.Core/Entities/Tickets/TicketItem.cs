using System;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.Tickets
{
    public class TicketItem : BaseEntity<long>
    {
        public User User { get; set; }
        public long UserId { get; set; }
        public Ticket Ticket { get; set; }
        public long TicketId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }=DateTime.Now;
        public bool IsSeen { get; set; }
    }
}