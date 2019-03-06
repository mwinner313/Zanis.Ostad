using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses.Commands.ChangeApprovalStatus
{
    public class ChangeCourseApprovalStatusCommandHandler:IRequestHandler<ChangeCourseApprovalStatusCommand,Response>
    {
        private readonly IRepository<Course, long> _repository;

        public ChangeCourseApprovalStatusCommandHandler(IRepository<Course, long> repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(ChangeCourseApprovalStatusCommand request, CancellationToken cancellationToken)
        {
            var course = _repository.GetQueriable().First(x => x.Id == request.CourseId);
            course.ApprovalStatus = request.CourseApprovalStatus;
            await _repository.EditAsync(course);
            return Response.Success();
        }
    }
}