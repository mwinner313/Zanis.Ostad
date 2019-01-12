using System.Collections.Generic;
using MediatR;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zains.Ostad.Application.Fields.Queries.GetFieldsList
{
    public class GetFieldsListQuery : IRequest<List<FieldListViewModel>>
    {
        public int? CollegeId { get; set; }
        public int? GradeId { get; set; }
        public ProductType ProductType { get; set; }
        public string SearchText { get; set; } = "";
    }
}