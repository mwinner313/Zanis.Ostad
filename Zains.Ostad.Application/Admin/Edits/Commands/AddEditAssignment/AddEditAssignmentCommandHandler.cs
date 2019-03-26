using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Admin.Edits.Commands.AddEditAssignment
{
    public class AddEditAssignmentCommandHandler : IRequestHandler<AddEditAssignmentByCourseItemCommand, Response>
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
            var courseItems = await GetCourseItems(request, cancellationToken);
            var alreadyAssignedItemsCount = 0;
            _unitOfWork.Begin();
            foreach (var item in courseItems)
            {
                if (IsAlreadyAssignedToSomeEditor(item))
                {
                    alreadyAssignedItemsCount++;
                    continue;
                }

                await UpdateCourseItemLatestState(item);
                await AddEditAssign(request, item);
            }

            _unitOfWork.Commit();
            return alreadyAssignedItemsCount > 0
                ? Response.Success($"{alreadyAssignedItemsCount}" + "مورد انتخابی در حال حاظر در دست تدوین است ")
                : Response.Success();
        }

        private async Task<List<CourseItem>> GetCourseItems(AddEditAssignmentByCourseItemCommand request,
            CancellationToken cancellationToken)
        {
            return await _courseItemRepo.GetQueryable().Where(x => request.CourseItemIds.Contains(x.Id))
                .ToListAsync(cancellationToken);
        }

        private async Task AddEditAssign(AddEditAssignmentByCourseItemCommand request, CourseItem item)
        {
            await _editAssignmentRepo.AddAsync(new EditAssignment
            {
                CourseItemId = item.Id,
                EditorId = request.EditorId
            });
        }

        private async Task UpdateCourseItemLatestState(CourseItem item)
        {
            item.LatestEditStatus = EditStatus.PendingToEdit;
            await _courseItemRepo.EditAsync(item);
        }

        private bool IsAlreadyAssignedToSomeEditor(CourseItem item)
        {
            return _editAssignmentRepo.GetQueryable()
                .Any(x => x.CourseItemId == item.Id && x.Status != EditStatus.RejectedByAdmin);
        }
    }
}