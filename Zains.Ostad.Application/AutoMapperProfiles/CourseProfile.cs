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
            CreateMap<CourseItem, CourseItemViewModel>();
        }

        public static Expression<Func<Course, CourseDto>> Projection => x => new CourseDto
        {
            Id = x.Id,
            TeacherId = x.TeacherLessonMapping.Teacher.Id,
            ZipFilesPath = x.ZipFilesPath,
            HasPendingItemToApprove = x.HasPendingItemToApprove,
            Description = x.Description,
            Price = x.Price,
            ApprovalStatus = x.ApprovalStatus,
            CreatedOn=x.CreatedOn,
            Teacher = x.TeacherLessonMapping.Teacher.FullName,
            AdminMessageForTeacher = x.AdminMessageForTeacher,
            TeacherMessageForAdmin = x.TeacherMessageForAdmin,
            TeacherAvatar = x.TeacherLessonMapping.Teacher.AvatarPath,
            Title = x.CourseTitle.Name + " - " + x.TeacherLessonMapping.LessonFieldMapping.Lesson.LessonName,
            GradeTitle = x.TeacherLessonMapping.LessonFieldMapping.Grade.Name,
            LessonTitle=x.TeacherLessonMapping.LessonFieldMapping.Lesson.LessonName,
            FieldName=x.TeacherLessonMapping.LessonFieldMapping.Field.Name,
            LessonCode = x.TeacherLessonMapping.LessonFieldMapping.Lesson.LessonCode,
            Contents = x.Contents.Select(c => new CourseItemViewModel
            {
                Id = c.Id,
                Title = c.Title,
                ContentType = c.ContentType,
                FilePath = c.FilePath,
                CourseId = c.CourseId,
                State = c.State,
                CreatedOn = c.CreatedOn,
                IsPreview = c.IsPreview,
                AdminMessageForTeacher = c.AdminMessageForTeacher,
                TeacherMessageForAdmin = c.TeacherMessageForAdmin,
                Order = c.Order
            }).OrderBy(o=>o.Order).ToList()
        };

        public static Expression<Func<Course, UserCourseDto>> ProjectionForUser => x => new UserCourseDto
        {
            Id = x.Id,
            LessonName = x.TeacherLessonMapping.LessonFieldMapping.Lesson.LessonName,
            Teacher = x.TeacherLessonMapping.Teacher.FullName,
            Description = x.Description,
            Title = x.CourseTitle.Name,
            Contents = x.Contents.Select(c => new CourseItemViewModel
            {
                FilePath = c.FilePath,
                Title = c.Title,
                State = c.State,
                ContentType = c.ContentType,
                Order = c.Order,
                CourseId = c.CourseId,
                IsPreview = c.IsPreview
            }).ToList()
        };
    }
}