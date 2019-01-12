using System;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Tickets.Dtos
{
    public class TicketListViewModel
    {
        public long Id { get; set; }
        public string TicketReason { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public TicketState State { get; set; }
        public long UserId { get; set; }
        public string UserUserName { get; set; }
        public string UserFullName { get; set; }
        public string UserAvatarPath { get; set; }
        public long? CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CategoryTitle { get; set; }
        public int CategoryId { get; set; }
    }
}