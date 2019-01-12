using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Zains.Ostad.Application.AutoMapperProfiles;
using Zains.Ostad.Application.Courses.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailsQuery, CourseDto>
    {
        private readonly IRepository<Course, long> _courseRepository;
        private readonly IRepository<StudentCourseMapping, long> _studentCoursesRepo;
        private readonly IRepository<LessonFieldMapping, long> _lessonFieldMappingRepo;

        public GetCourseDetailsQueryHandler(IRepository<Course, long> courseRepository,
            IRepository<StudentCourseMapping, long> studentCoursesRepo,
            IRepository<LessonFieldMapping, long> lessonFieldMappingRepo)
        {
            _courseRepository = courseRepository;
            _studentCoursesRepo = studentCoursesRepo;
            _lessonFieldMappingRepo = lessonFieldMappingRepo;
        }

        public async Task<CourseDto> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            var course = _courseRepository.GetQueriable().Select(CourseProfile.Projection)
                .First(x => x.Id == request.CourseId);
            if (request.CurrentUserId.HasValue)
                course.IsOwnedByCurrentUser = _studentCoursesRepo.GetQueriable().Any(x =>
                    x.CourseId == request.CourseId && x.StudentId == request.CurrentUserId.Value);
            course.RelatedFields = _lessonFieldMappingRepo.GetQueriable().Where(x =>
                    x.Lesson.LessonCode == course.LessonCode && x.Grade.Name == course.GradeTitle)
                .Select(x => x.Field.Name)
                .ToList();
            return course;
        }
    }
}