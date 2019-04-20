using MediatR;

namespace Zains.Ostad.Application.Colleges.Queries.GetCollegeList
{
    public class GetCollegeDetailsQuery : IRequest<CollegeListViewModel>
    {
        public int CollegeId { get; set; }
    }
}