using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.TicketCategories.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.TicketCategories.Queries.GetTicketCategories
{
    public class GetTicketCategoriesQueryHandler : IRequestHandler<GetTicketCategoriesQuery, List<TicketCategoryViewModel>>
    {
        private readonly IRepository<TicketCategory, int> _repository;
        private readonly IMapper _mapper;
        public GetTicketCategoriesQueryHandler(IRepository<TicketCategory, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<List<TicketCategoryViewModel>> Handle(GetTicketCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var dbQuery = _repository.GetQueryable();
            if (request.Type.HasValue)
                dbQuery = dbQuery.Where(x => x.CategoryType == request.Type);
            return dbQuery.ProjectTo<TicketCategoryViewModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}