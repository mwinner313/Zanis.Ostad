using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Edits.Commands.AddEditAssignment
{
    public class AddEditAssignmentByCourseCommand : IRequest<Response>
    {
        public long CourseId { get; set; }
        public long EditorId { get; set; }
    }
}