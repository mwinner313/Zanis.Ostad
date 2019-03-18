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

        public AddEditAssignmentCommandHandler(IRepository<EditAssignment, long> editAssignmentRepo,
            IRepository<CourseItem, long> courseItemRepo)
        {
            _editAssignmentRepo = editAssignmentRepo;
            _courseItemRepo = courseItemRepo;
        }

        public async Task<Response> Handle(AddEditAssignmentByCourseItemCommand request,
            CancellationToken cancellationToken)
        {
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
            var courseItemIds = _courseItemRepo.GetQueriable().Where(x => x.CourseId == request.CourseId)
                .Select(x => x.Id).ToList();
            
            foreach (var itemId in courseItemIds)
            {
                await _editAssignmentRepo.AddAsync(new EditAssignment
                {
                    EditorId = request.EditorId,
                    CourseItemId = itemId
                });
            }

            return Response.Success();
        }
    }
}