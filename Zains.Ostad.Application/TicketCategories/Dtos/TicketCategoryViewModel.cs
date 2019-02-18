using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.TicketCategories.Dtos
{
    public class TicketCategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TicketCategoryType CategoryType { get; set; }
    }
}