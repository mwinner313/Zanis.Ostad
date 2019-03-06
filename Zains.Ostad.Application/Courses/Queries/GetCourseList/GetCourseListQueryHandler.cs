using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.AutoMapperProfiles;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses.Queries.GetCourseList
{
    public class GetCourseListQueryHandler:IRequestHandler<GetCourseListQuery,PagenatedList<CourseDto>>
    {
        private readonly IRepository<Course, long> _repository;

        public GetCourseListQueryHandler(IRepository<Course, long> repository)
        {
            _repository = repository;
        }

        public async Task<PagenatedList<CourseDto>> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
        {
            var queryable = _repository.GetQueriable().OrderByDescending(x => x.CreatedOn);
            return new PagenatedList<CourseDto>
            {
                Items = await queryable.Select(CourseProfile.Projection).Pagenate(request).ToListAsync(cancellationToken),
                AllCount = queryable.Count()
            };
        }
    }
}