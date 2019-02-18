using AutoMapper;
using Zains.Ostad.Application.TicketCategories.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.AutoMapperProfiles
{
    public class TicketCategoryProfile : Profile
    {
        public TicketCategoryProfile()
        {
            CreateMap<TicketCategory, TicketCategoryViewModel>();
        }
    }
}