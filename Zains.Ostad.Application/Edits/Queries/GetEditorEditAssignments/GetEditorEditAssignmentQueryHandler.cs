using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zains.Ostad.Application.Editors.Queries.GetEditAssignments;
using Zains.Ostad.Application.Infrastucture;

namespace Zains.Ostad.Application.Edits.Queries.GetEditorEditAssignments
{
    public class GetEditorEditAssignmentQueryHandler:IRequestHandler<GetEditorEditAssignmentQuery,PagenatedList<EditAssignmentViewModel>>
    {
        public Task<PagenatedList<EditAssignmentViewModel>> Handle(GetEditorEditAssignmentQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}