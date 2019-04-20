using MediatR;

namespace Zains.Ostad.Application.Fields.Queries.GetFieldsList
{
    public class GetFieldQuery : IRequest<FieldListViewModel>
    {
        public long FieldId { get; set; }
    }
}
