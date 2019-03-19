using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Admin.Edits.Commands.ChangeEditAssignmentStatus
{
    public class
        ChangeEditAssignmentStatusCommandHandler : IRequestHandler<ChanageEditAssignmentStatusCommand, Response>
    {
        private readonly IRepository<EditAssignment, long> _editAssignmentRepo;
        private readonly IRepository<CourseItem, long> _courseItemRepo;

        public ChangeEditAssignmentStatusCommandHandler(IRepository<EditAssignment, long> editAssignmentRepo,
            IRepository<CourseItem, long> courseItemRepo)
        {
            _editAssignmentRepo = editAssignmentRepo;
            _courseItemRepo = courseItemRepo;
        }

        public async Task<Response> Handle(ChanageEditAssignmentStatusCommand request,
            CancellationToken cancellationToken)
        {
            var item = await _editAssignmentRepo.GetById(request.EditAssignmentId);
            await ChanageEditStaus(request, item);
            await ChangeLatestEditStatusForCourseItemIfRequired(request, item);
            return Response.Success();
        }

        private async Task ChanageEditStaus(ChanageEditAssignmentStatusCommand request, EditAssignment item)
        {
            item.Status = request.Status;
            await _editAssignmentRepo.EditAsync(item);
        }

        private async Task ChangeLatestEditStatusForCourseItemIfRequired(ChanageEditAssignmentStatusCommand request,
            EditAssignment item)
        {
            if (_editAssignmentRepo.GetQueryable().Where(x =>
                    x.CourseItemId == request.EditAssignmentId && x.Id != request.EditAssignmentId)
                .All(x => x.CreatedOn < item.CreatedOn))
            {
                var courseItem = await _courseItemRepo.GetById(item.Id);
                courseItem.LatestEditStatus = request.Status;
                await _courseItemRepo.EditAsync(courseItem);
            }
        }
    }
}