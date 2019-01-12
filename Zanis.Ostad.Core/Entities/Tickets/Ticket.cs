using System;
using System.Collections.Generic;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.Tickets
{
    public class Ticket:BaseEntity<long>
    {
        public ICollection<TicketItem> TicketItems { get; set; }
        public TicketCategory Category { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string TicketReason { get; set; }
        public TicketState State { get; set; }
        public Course Course { get; set; }
        public long? CourseId { get; set; }
    }
}