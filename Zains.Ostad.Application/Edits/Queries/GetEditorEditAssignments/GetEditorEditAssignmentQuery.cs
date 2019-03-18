using MediatR;
using Zains.Ostad.Application.Editors.Queries.GetEditAssignments;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Edits.Queries.GetEditorEditAssignments
{
    public class GetEditorEditAssignmentQuery:Pagenation, IRequest<PagenatedList<EditAssignmentViewModel>>
    {
        public long EditorId { get; set; }
        public EditStatus? EditStatus { get; set; }
    }
}