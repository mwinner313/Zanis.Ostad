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
            var queryable = _repository.GetQueryable().OrderByDescending(x => x.CreatedOn).AsQueryable();
            
            queryable = ApplyFilter(request, queryable);

            return new PagenatedList<CourseDto>
            {
                Items = await queryable.Select(CourseProfile.Projection).Pagenate(request)
                    .ToListAsync(cancellationToken),
                AllCount = queryable.Count()
            };
        }

        private static IQueryable<Course> ApplyFilter(GetCourseListQuery request, IQueryable<Course> queryable)
        {
            if (request.Status.HasValue)
                queryable = queryable.Where(x => x.ApprovalStatus == request.Status);

            if (!string.IsNullOrEmpty(request.TeacherUserName))
                queryable = queryable.Where(x =>
                    x.TeacherLessonMapping.Teacher.UserName.Contains(request.TeacherUserName) ||
                    x.TeacherLessonMapping.Teacher.FullName.Contains(request.TeacherUserName));

            if (!string.IsNullOrEmpty(request.LessonCode))
                queryable = queryable.Where(x =>
                    x.TeacherLessonMapping.LessonFieldMapping.Lesson.LessonCode.Contains(request.LessonCode));

            if (!string.IsNullOrEmpty(request.FieldCode))
                queryable = queryable.Where(x =>
                    x.TeacherLessonMapping.LessonFieldMapping.Field.Code.Contains(request.FieldCode));

            if (request.GradeId.HasValue)
                queryable = queryable.Where(x =>
                    x.TeacherLessonMapping.LessonFieldMapping.GradeId == request.GradeId);
            
            if (request.CourseTitleId.HasValue)
                queryable = queryable.Where(x =>
                    x.CourseTitleId == request.CourseTitleId);
            
            return queryable;
        }
    }
}