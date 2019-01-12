using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.General.Database
{
    public class InitDataBaseCommandHandler : IRequestHandler<InitDataBaseCommand, Response>
    {
        private readonly IRepository<TicketCategory, int> _ticketCategoryRepo;

        public InitDataBaseCommandHandler(IRepository<TicketCategory, int> ticketCategoryRepo)
        {
            _ticketCategoryRepo = ticketCategoryRepo;
        }

        public async Task<Response> Handle(InitDataBaseCommand request, CancellationToken cancellationToken)
        {
            if (_ticketCategoryRepo.GetQueriable().All(x => x.CatetgoryType != CatetgoryType.RelatedToTeacher))
                await _ticketCategoryRepo.AddAsync(new TicketCategory
                {
                    Title = "دوره اموزشی استاد",
                    CatetgoryType = CatetgoryType.RelatedToTeacher,
                    IsDeletible = false
                });
            if (_ticketCategoryRepo.GetQueriable().All(x => x.CatetgoryType != CatetgoryType.RelatedToSupport))
                await _ticketCategoryRepo.AddAsync(new TicketCategory
                {
                    Title = "پشتیبانی",
                    CatetgoryType = CatetgoryType.RelatedToSupport,
                    IsDeletible = false
                });
            return Response.Success();
        }
    }
}