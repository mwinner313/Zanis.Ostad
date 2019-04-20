using MediatR;

namespace Zains.Ostad.Application.Grades.Queries.GetGrades
{
    public class GetGradeQuery : IRequest<GradeViewModel>
    {
        public int GradeId { get; set; }
    }
}
