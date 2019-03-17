using MediatR;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Editors.Queries.GetEditAssignments
{
    public class GetEditAssignmentsQuery:Pagenation,IRequest<PagenatedList<EditAssignmentViewModel>>
    {
        public EditStatus? Status { get; set; }
        public string Search { get; set; }
    }
}