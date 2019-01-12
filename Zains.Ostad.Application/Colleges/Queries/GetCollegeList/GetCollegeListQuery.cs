using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zains.Ostad.Application.Colleges.Queries.GetCollegeList
{
    public class GetCollegeQuery:IRequest<List<CollegeListViewModel>>
    {
        [Required]
        public int? GradeId { get; set; }
        public ProductType ProductType { get; set; }
        public string SearchText { get; set; } = "";
    }
}