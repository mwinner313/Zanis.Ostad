using System.Collections.Generic;
using MediatR;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zains.Ostad.Application.Grades.Queries.GetGrades
{
    public class GetGradesQuery:IRequest<List<GradeViewModel>>
    {
        public ProductType ProductType { get; set; }
    }
}