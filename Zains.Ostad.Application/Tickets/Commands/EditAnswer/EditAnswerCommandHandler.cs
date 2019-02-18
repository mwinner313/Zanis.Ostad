using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Tickets.Commands.EditAnswer
{
    public class EditAnswerCommandHandler:IRequestHandler<EditAnswerCommand,Response<TicketItemViewModel>>
    {
        private readonly IRepository<TicketItem, long> _ticketItemRepo;
        private readonly IMapper _mapper;
        public EditAnswerCommandHandler(IRepository<TicketItem, long> ticketItemRepo, IMapper mapper)
        {
            _ticketItemRepo = ticketItemRepo;
            _mapper = mapper;
        }

        public async Task<Response<TicketItemViewModel>> Handle(EditAnswerCommand request, CancellationToken cancellationToken)
        {
            var ticketItem = await _ticketItemRepo.GetById(request.TicketItemId);
            ticketItem.Message = request.Message;
            ticketItem.IsEdited = true;
            await _ticketItemRepo.EditAsync(ticketItem);
            return Response<TicketItemViewModel>.Success(_mapper.Map<TicketItemViewModel>(ticketItem));
        }
    }
}