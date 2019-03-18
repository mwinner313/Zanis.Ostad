using System.Collections.Generic;
using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Edits.Commands.AddEditAssignment
{
    public class AddEditAssignmentByCourseItemCommand:IRequest<Response>
    {
        public List<long> CourseItemIds { get; set; }
        public long EditorId { get; set; }
    }
}