using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Lessons.Queries.GetLessonList;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Entities.Orders;

namespace Zains.Ostad.Application.AutoMapperProfiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseItem, CourseItemViewModel>();
        }

        public static Expression<Func<Course, CourseDto>> ProjectionList => x => new CourseDto
        {
            Id = x.Id,
            TeacherId = x.TeacherId,
            HasPendingItemToApprove = x.HasPendingItemToApprove,
            Description = x.Description,
            Price = x.Price,
            Permalink = x.PermaLink,
            ImagePath = x.ImagePath,
            Duration = new TimeSpan(x.Contents.Sum(c => c.VideoDuration.Ticks)),
            ApprovalStatus = x.ApprovalStatus,
            CreatedOn = x.CreatedOn,
            Teacher = x.Teacher.FullName,
            AdminMessageForTeacher = x.AdminMessageForTeacher,
            TeacherMessageForAdmin = x.TeacherMessageForAdmin,
            TeacherAvatar = x.Teacher.AvatarPath,
            Title =  x.Title,
            RelatedLessonFields = x.Lessons.Select(l => new LessonFieldViewModel
            {
                Id = l.Lesson.Id,
                LessonName = l.Lesson.Lesson.LessonName,
                LessonId = l.Lesson.LessonId,
                FieldName = l.Lesson.Field.Name,
                FieldId = l.Lesson.FieldId,
                GradeId = l.Lesson.GradeId,
                GradeName = l.Lesson.Grade.Name
            }).ToList()
        };

        public static Expression<Func<Course, CourseDto>> Projection => x => new CourseDto
        {
            Id = x.Id,
            CourseCategoryId=x.CourseCategoryId,
            TeacherId = x.TeacherId,
             Permalink = x.PermaLink,
            HasPendingItemToApprove = x.HasPendingItemToApprove,
            Description = x.Description,
            Price = x.Price,
            ApprovalStatus = x.ApprovalStatus,
            CreatedOn = x.CreatedOn,
            Teacher = x.Teacher.FullName,
            AdminMessageForTeacher = x.AdminMessageForTeacher,
            TeacherMessageForAdmin = x.TeacherMessageForAdmin,
            TeacherAvatar = x.Teacher.AvatarPath,
            Title =  x.Title,
            CourseCategoryTitle=x.CourseCategory.Name ,
            RelatedLessonFields = x.Lessons.Select(l => new LessonFieldViewModel
            {
                Id = l.Lesson.Id,
                LessonName = l.Lesson.Lesson.LessonName,
                LessonId = l.Lesson.LessonId,
                FieldName = l.Lesson.Field.Name,
                FieldId = l.Lesson.FieldId,
                GradeId = l.Lesson.GradeId,
                GradeName = l.Lesson.Grade.Name
            }).ToList(),
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
            Title = x.CourseCategory.Name,
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