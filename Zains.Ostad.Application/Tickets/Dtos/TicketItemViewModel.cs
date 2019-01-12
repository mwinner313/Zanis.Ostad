using System;

namespace Zains.Ostad.Application.Tickets.Dtos
{
    public class TicketItemViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserUserName { get; set; }
        public string UserFullName { get; set; }
        public string UserAvatarPath { get; set; }
        public long TicketId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}