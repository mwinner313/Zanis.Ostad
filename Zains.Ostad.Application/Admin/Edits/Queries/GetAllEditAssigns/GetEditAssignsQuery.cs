using MediatR;
using Zains.Ostad.Application.Editors.Queries.GetEditAssignments;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Admin.Edits.Queries.GetAllEditAssigns
{
    public class GetEditAssignsQuery:Pagenation,IRequest<PagenatedList<EditAssignmentViewModel>>
    {
        public string Search { get; set; }
        public EditStatus? Status { get; set; }
    }
}