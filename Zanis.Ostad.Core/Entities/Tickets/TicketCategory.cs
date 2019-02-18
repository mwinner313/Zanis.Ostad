using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.Tickets
{
    public class TicketCategory:BaseEntity<int>
    {
        public string Title { get; set; }
        public TicketCategoryType CategoryType { get; set; }
        public bool IsDeletible { get; set; }
    }
}