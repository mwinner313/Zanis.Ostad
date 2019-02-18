using System.Collections.Generic;

namespace Zains.Ostad.Application.Tickets.Dtos
{
    public class TicketDto
    {
        public List<TicketListViewModel> Items { get; set; }
        public TicketMetaData MetaData { get; set; }
    }
}