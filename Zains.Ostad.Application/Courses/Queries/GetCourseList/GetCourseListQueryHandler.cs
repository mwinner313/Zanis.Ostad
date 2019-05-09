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
    public class GetCourseListQueryHandler : IRequestHandler<GetCourseListQuery, PagenatedList<CourseDto>>
    {
        private readonly IRepository<Course, long> _repository;

        public GetCourseListQueryHandler(IRepository<Course, long> repository)
        {
            _repository = repository;
        }

        public async Task<PagenatedList<CourseDto>> Handle(GetCourseListQuery request,
            CancellationToken cancellationToken)
        {
            var queryable = _repository.GetQueryable();
            
            queryable = ApplyFilter(request, queryable).Include(x => x.Teacher)
                .Include(x => x.CourseCategory)
                .Include(x=>x.Contents)
                .Include(x => x.Lessons).ThenInclude(x => x.Lesson)
                .Include(x => x.Lessons).ThenInclude(x => x.Lesson.Lesson)
                .Include(x => x.Lessons).ThenInclude(x => x.Lesson.Field)
                .Include(x => x.Lessons).ThenInclude(x => x.Lesson.Grade);
            var items = await queryable.OrderByDescending(x=>x.Id).Pagenate(request).Select(CourseProfile.ProjectionList)
                .ToListAsync(cancellationToken);
            return new PagenatedList<CourseDto>
            {
                Items = items,
                AllCount = queryable.Count()
            };
        }

        private static IQueryable<Course> ApplyFilter(GetCourseListQuery request, IQueryable<Course> queryable)
        {
            if (request.Status.HasValue)
                queryable = queryable.Where(x => x.ApprovalStatus == request.Status);

            if (!string.IsNullOrEmpty(request.TeacherUserName))
                queryable = queryable.Where(x =>
                    x.Teacher.UserName.Contains(request.TeacherUserName) ||
                    x.Teacher.FullName.Contains(request.TeacherUserName));

            if (!string.IsNullOrEmpty(request.LessonCode))
                queryable = queryable.Where(x =>
                    x.Lessons.Any(l => l.Lesson.Lesson.LessonCode.Contains(request.LessonCode)));
            
            if (request.FieldId.HasValue)
                queryable = queryable.Where(x =>
                    x.Lessons.Any(l => l.Lesson.FieldId==request.FieldId));
            
            if (!string.IsNullOrEmpty(request.FieldCode))
                queryable = queryable.Where(x =>
                    x.Lessons.Any(l => l.Lesson.Field.Code.Contains(request.FieldCode)));

            if (request.GradeId.HasValue)
                queryable = queryable.Where(x =>
                    x.Lessons.Any(l => l.Lesson.GradeId == request.GradeId));

            if (request.CourseCategoryId.HasValue)
                queryable = queryable.Where(x =>
                    x.CourseCategoryId == request.CourseCategoryId);

            return queryable;
        }
    }
}