using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Editors.Commands.UploadEditedItem
{
    public class UploadEditedItemCommandHandler : IRequestHandler<UploadEditedItemCommand, Response>
    {
        private readonly IRepository<EditAssignment, long> _editAssignmentRepo;
        private readonly ICoursesFileManager _coursesFileManager;

        public UploadEditedItemCommandHandler(IRepository<EditAssignment, long> editAssignmentRepo,
            ICoursesFileManager coursesFileManager)
        {
            _editAssignmentRepo = editAssignmentRepo;
            _coursesFileManager = coursesFileManager;
        }

        public async Task<Response> Handle(UploadEditedItemCommand request, CancellationToken cancellationToken)
        {
            var ea = await _editAssignmentRepo.GetQueriable().Include(x => x.CourseItem)
                .FirstAsync(x => x.Id == request.EditAssignmnetId, cancellationToken);
            ea.Status = EditStatus.PendingToApproveByAdmin;
            await _coursesFileManager.SaveEditedFileFileByEditor(request.File, ea.CourseItem.CourseId);
            ea.FilePath = await _coursesFileManager.GetEditedFilePathForDownload(request.File, ea.CourseItem.CourseId);
            await _editAssignmentRepo.EditAsync(ea);
            return Response.Success();
        }
    }
}