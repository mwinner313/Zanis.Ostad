using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zains.Ostad.Application.AutoMapperProfiles;
using Zains.Ostad.Application.Editors.Queries.GetEditAssignments;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Admin.Edits.Queries.GetEditorEditAssignments
{
    public class GetEditorEditAssignmentQueryHandler:IRequestHandler<GetEditorEditAssignmentQuery,PagenatedList<EditAssignmentViewModel>>
    {
        private readonly IRepository<EditAssignment, long> _editAssignRepository;

        public GetEditorEditAssignmentQueryHandler(IRepository<EditAssignment, long> editAssignRepository)
        {
            _editAssignRepository = editAssignRepository;
        }

        public async Task<PagenatedList<EditAssignmentViewModel>> Handle(GetEditorEditAssignmentQuery request, CancellationToken cancellationToken)
        {
            var queriable = _editAssignRepository.GetQueryable().Where(x => x.EditorId == request.EditorId);
            if (request.EditStatus.HasValue)
                queriable = queriable.Where(x => x.Status == request.EditStatus);
            
            return new PagenatedList<EditAssignmentViewModel>
            {
                AllCount = queriable.Count(),
                Items = queriable.OrderByDescending(x=>x.CreatedOn).Pagenate(request).Select(EditAssignmentProfile.Projection).ToList()
            };
        }
    }
}