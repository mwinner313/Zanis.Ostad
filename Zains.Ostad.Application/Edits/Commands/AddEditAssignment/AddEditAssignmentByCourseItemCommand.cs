using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Edits.Commands
{
    public class AddEditAssignmentByCourseItemCommand:IRequest<Response>
    {
        public long  CourseItemId { get; set; }
        public long EditorId { get; set; }
    }
}