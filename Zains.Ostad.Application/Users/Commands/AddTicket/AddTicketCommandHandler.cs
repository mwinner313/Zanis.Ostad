using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Users.Commands.AddTicket
{
    public class AddTicketCommandHandler : IRequestHandler<AddTicketCommand, Response<TicketViewModel>>
    {
        private readonly IWorkContext _workContext;
        private readonly IRepository<TicketCategory, int> _ticktCategoryRepository;
        private readonly IRepository<Ticket, long> _ticketRepo;
        private readonly IMapper _mapper;

        public AddTicketCommandHandler(IWorkContext workContext,
            IRepository<TicketCategory, int> ticktCategoryRepository, IRepository<Ticket, long> ticketRepo, IMapper mapper)
        {
            _workContext = workContext;
            _ticktCategoryRepository = ticktCategoryRepository;
            _ticketRepo = ticketRepo;
            _mapper = mapper;
        }

        public async Task<Response<TicketViewModel>> Handle(AddTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = CreateTicket(request);
            await _ticketRepo.AddAsync(ticket);
            return Response<TicketViewModel>.Success(_mapper.Map<TicketViewModel>(_ticketRepo.GetQueriable().Include(x=>x.User).Single(x=>x.Id==ticket.Id)));
        }

        private Ticket CreateTicket(AddTicketCommand request)
        {
            var ticket = new Ticket
            {
                LastMessageText = request.Description,
                UserId = _workContext.CurrentUserId,
                State = TicketState.Open,
                TicketReason = request.Title,
                TicketItems = new List<TicketItem>
                {
                    new TicketItem
                    {
                        UserId = _workContext.CurrentUserId,
                        Message = request.Description,
                    }
                }
            };
            if (request.CourseId.HasValue)
            {
                ticket.CategoryId = _ticktCategoryRepository.GetQueriable()
                    .First(x => x.CatetgoryType == CatetgoryType.RelatedToTeacher).Id;
            }
            else
            {
                ticket.CategoryId = request.CategoryId.HasValue
                    ? request.CategoryId.Value
                    : _ticktCategoryRepository.GetQueriable()
                        .First(x => x.CatetgoryType == CatetgoryType.RelatedToSupport).Id;
            }

            return ticket;
        }
    }
}