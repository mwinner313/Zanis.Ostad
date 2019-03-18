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

namespace Zains.Ostad.Application.Teachers.Queries.GetProducedCourses
{
    public class GetProducedCoursesQueryHandler : IRequestHandler<GetProducedCoursesQuery, PagenatedList<CourseDto>>,
        IRequestHandler<GetProducedCourseDetailQuery, CourseDto>
    {
        private readonly IRepository<Course, long> _courseRepo;
        private readonly IWorkContext _workContext;

        public GetProducedCoursesQueryHandler(IWorkContext workContext, IRepository<Course, long> courseRepo)
        {
            _workContext = workContext;
            _courseRepo = courseRepo;
        }

        public async Task<PagenatedList<CourseDto>> Handle(GetProducedCoursesQuery request,
            CancellationToken cancellationToken)
        {
            var queriable = _courseRepo.GetQueryable()
                .Where(x => x.TeacherLessonMapping.TeacherId == _workContext.CurrentUserId);
            return new PagenatedList<CourseDto>
            {
                AllCount = queriable.Count(),
                Items = queriable.Pagenate(request).Select(CourseProfile.Projection).ToList()
            };
        }

        public Task<CourseDto> Handle(GetProducedCourseDetailQuery request, CancellationToken cancellationToken)
        {
            return _courseRepo.GetQueryable().Select(CourseProfile.Projection)
                .FirstAsync(x => x.Id == request.CourseId && x.TeacherId == _workContext.CurrentUserId , cancellationToken);
        }
    }
}