using MediatR;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Edits.Commands.ChangeEditAssignmentStatus
{
    public class ChanageEditAssignmentStatusCommand : IRequest<Response>
    {
        public long EditAssignmentId { get; set; }
        public EditStatus Status { get; set; }
    }
}