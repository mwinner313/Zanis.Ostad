using MediatR;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses.Commands.ChangeApprovalStatus
{
    public class ChangeCourseApprovalStatusCommand:IRequest<Response>
    {
        public long CourseId { get; set; }
        public CourseApprovalStatus CourseApprovalStatus { get; set; }
        public string AdminMessageForTeacher { get; set; }
        public bool UpdateMessage { get; set; }
    }
}