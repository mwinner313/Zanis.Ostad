using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Edits.Commands.AddEditAssignment
{
    public class AddEditAssignmentCommandHandler : IRequestHandler<AddEditAssignmentByCourseItemCommand, Response>,
        IRequestHandler<AddEditAssignmentByCourseCommand, Response>
    {
        private readonly IRepository<EditAssignment, long> _editAssignmentRepo;
        private readonly IRepository<CourseItem, long> _courseItemRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AddEditAssignmentCommandHandler(IRepository<EditAssignment, long> editAssignmentRepo,
            IRepository<CourseItem, long> courseItemRepo, IUnitOfWork unitOfWork)
        {
            _editAssignmentRepo = editAssignmentRepo;
            _courseItemRepo = courseItemRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(AddEditAssignmentByCourseItemCommand request,
            CancellationToken cancellationToken)
        {
            var courseItem = await _courseItemRepo.GetById(request.CourseItemId);
            courseItem.LatestEditStatus = EditStatus.PendingToEdit;
            await _courseItemRepo.EditAsync(courseItem);
            await _editAssignmentRepo.AddAsync(new EditAssignment
            {
                CourseItemId = request.CourseItemId,
                EditorId = request.EditorId
            });
            return Response.Success();
        }

        public async Task<Response> Handle(AddEditAssignmentByCourseCommand request,
            CancellationToken cancellationToken)
        {
            var courseItems = _courseItemRepo.GetQueriable().Where(x => x.CourseId == request.CourseId)
                .ToList();

            _unitOfWork.Begin();
            foreach (var item in courseItems)
            {
                await _editAssignmentRepo.AddAsync(new EditAssignment
                {
                    EditorId = request.EditorId,
                    CourseItemId = item.Id
                });
                item.LatestEditStatus = EditStatus.PendingToEdit;
                await _courseItemRepo.EditAsync(item);
            }
            _unitOfWork.Commit();
            return Response.Success();
        }
    }
}