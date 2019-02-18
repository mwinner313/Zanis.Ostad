using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Lessons.Queries.GetLesson;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.AutoMapperProfiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
        }

        public static Expression<Func<Course, CourseDto>> Projection => x => new CourseDto()
        {
            Id = x.Id,
            Description = x.Description,
            Price = x.Price,
            Teacher = x.TeacherLessonMapping.Teacher.FullName,
            TeacherAvatar = x.TeacherLessonMapping.Teacher.AvatarPath,
            Title = x.CourseTitle.Name + " - " + x.TeacherLessonMapping.LessonFieldMapping.Lesson.LessonName,
            GradeTitle = x.TeacherLessonMapping.LessonFieldMapping.Grade.Name,
            LessonCode = x.TeacherLessonMapping.LessonFieldMapping.Lesson.LessonCode,
            Contents = x.Contents.Select(c => new CourseItemDto
            {
                Id = c.Id,
                Title = c.Title,
                ContentType = c.ContentType,
                FilePath = c.FilePath,
                CourseId = c.CourseId,
                IsPreview = c.IsPreview
            }).ToList()
        };

        public static Expression<Func<Course, UserCourseDto>> ProjectionForUser => x => new UserCourseDto()
        {
            Id = x.Id,
            LessonName = x.TeacherLessonMapping.LessonFieldMapping.Lesson.LessonName,
            Teacher = x.TeacherLessonMapping.Teacher.FullName,
            Description = x.Description,
            Title = x.CourseTitle.Name,
            Contents = x.Contents.Select(c => new CourseItemDto
            {
                FilePath = c.FilePath,
                Title = c.Title,
                ContentType = c.ContentType,
                CourseId = c.CourseId,
                IsPreview = c.IsPreview
            }).ToList()
        };
    }
}