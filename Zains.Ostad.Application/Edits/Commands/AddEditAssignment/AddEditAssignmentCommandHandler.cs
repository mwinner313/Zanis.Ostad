using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Edits.Commands.AddEditAssignment
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
            var courseItems = await _courseItemRepo.GetQueryable().Where(x => request.CourseItemIds.Contains(x.Id))
                .ToListAsync( cancellationToken);
            _unitOfWork.Begin();
            foreach (var item in courseItems)
            {
                item.LatestEditStatus = EditStatus.PendingToEdit;
                await _courseItemRepo.EditAsync(item);
                await _editAssignmentRepo.AddAsync(new EditAssignment
                {
                    CourseItemId = item.Id,
                    EditorId = request.EditorId
                });
            }
            _unitOfWork.Commit();
            return Response.Success();
        }

      
    }
}