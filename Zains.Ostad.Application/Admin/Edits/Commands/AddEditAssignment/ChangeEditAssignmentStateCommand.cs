using MediatR;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Admin.Edits.Commands.AddEditAssignment
{
    public class ChangeEditAssignmentStateCommand:IRequest<Response>
    {
        public long EditId { get; set; }
        public EditStatus Status { get; set; }
    }
}