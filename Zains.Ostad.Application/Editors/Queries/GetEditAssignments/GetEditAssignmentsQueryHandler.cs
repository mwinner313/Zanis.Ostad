using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.AutoMapperProfiles;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Editors.Queries.GetEditAssignments
{
    public class GetEditAssignmentsQueryHandler : IRequestHandler<GetEditAssignmentsQuery, PagenatedList<EditAssignmentViewModel>>
    {
        private readonly IRepository<EditAssignment, long> _repository;
        private readonly IWorkContext _workContext;

        public GetEditAssignmentsQueryHandler(IRepository<EditAssignment, long> repository, IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }

        public async Task<PagenatedList<EditAssignmentViewModel>> Handle(GetEditAssignmentsQuery request,
            CancellationToken cancellationToken)
        {
            var queryable = _repository.GetQueriable().Where(x => x.EditorId == _workContext.CurrentUserId)
                .OrderByDescending(x => x.CreatedOn);

            return new PagenatedList<EditAssignmentViewModel>
            {
                Items = queryable.Include(x=>x.CourseItem)
                    .Select(EditAssignmentProfile.Projection).AsQueryable().Pagenate(request).ToList(),
                AllCount = queryable.Count()
            };
        }
    }
}