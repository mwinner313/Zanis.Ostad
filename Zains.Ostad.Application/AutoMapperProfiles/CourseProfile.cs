using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Zains.Ostad.Application.Courses.Dtos;
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

        public static Expression<Func<Course, CourseDto>>     Projection => x => new CourseDto
        {
            Id = x.Id,
            TeacherId = x.TeacherId,
            HasPendingItemToApprove = x.HasPendingItemToApprove,
            Description = x.Description,
            Price = x.Price,
            ApprovalStatus = x.ApprovalStatus,
            CreatedOn = x.CreatedOn,
            Teacher = x.Teacher.FullName,
            AdminMessageForTeacher = x.AdminMessageForTeacher,
            TeacherMessageForAdmin = x.TeacherMessageForAdmin,
            TeacherAvatar = x.Teacher.AvatarPath,
            Title = x.CourseTitle.Name + " - " + x.Lessons.First().Lesson.Lesson.LessonName,
            GradeTitle = x.Lessons.First().Lesson.Grade.Name,
            LessonTitle = x.Lessons.First().Lesson.Lesson.LessonName,
            FieldName = x.Lessons.First().Lesson.Field.Name,
            LessonCode = x.Lessons.First().Lesson.Lesson.LessonCode,
            Contents = x.Contents.Select(c =>
                new CourseItemViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    ContentType = c.ContentType,
                    FilePath = c.FilePath,
                    CourseId = c.CourseId,
                    State = c.State,
                    CreatedOn = c.CreatedOn,
                    LatestEditStatus = c.LatestEditStatus,
                    IsPreview = c.IsPreview,
                    AdminMessageForTeacher = c.AdminMessageForTeacher,
                    TeacherMessageForAdmin = c.TeacherMessageForAdmin,
                    Order = c.Order
                }).OrderBy(o => o.Order).ToList()
        };

        public static Expression<Func<CourseItem, CourseItemViewModel>> CourseItemProjection => c =>
            new CourseItemViewModel
            {
                Id = c.Id,
                Title = c.Title,
                ContentType = c.ContentType,
                FilePath = c.FilePath,
                CourseId = c.CourseId,
                State = c.State,
                CreatedOn = c.CreatedOn,
                IsPreview = c.IsPreview,
                LatestEditStatus = c.LatestEditStatus,
                AdminMessageForTeacher = c.AdminMessageForTeacher,
                TeacherMessageForAdmin = c.TeacherMessageForAdmin,
                Order = c.Order
            };

        public static Expression<Func<Course, UserCourseDto>> ProjectionForUser => x => new UserCourseDto
        {
            Id = x.Id,
            LessonName = x.Lessons.First().Lesson.Lesson.LessonName,
            Teacher = x.Teacher.FullName,
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