using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.TicketCategories.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.TicketCategories.Queries.GetTicketCategories
{
    public class GetTicketCategoriesQuery:IRequest<List<TicketCategoryViewModel>>
    {
        public TicketCategoryType? Type { get; set; }
    }
}