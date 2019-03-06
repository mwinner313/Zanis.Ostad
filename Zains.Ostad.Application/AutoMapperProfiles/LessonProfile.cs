using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.ExamSamples.Dto;
using Zains.Ostad.Application.Lessons.Dtos;
using Zains.Ostad.Application.Lessons.Queries.GetLesson;
using Zains.Ostad.Application.Lessons.Queries.GetLessonList;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.AutoMapperProfiles
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
        }

        public static Expression<Func<LessonFieldMapping, LessonFieldListViewModel>> ProjectionList = x =>
            new LessonFieldListViewModel
            {
                LessonCode = x.Lesson.LessonCode,
                LessonId = x.LessonId,
                Id = x.Id,
                LessonName = x.Lesson.LessonName
            };

        public static Expression<Func<LessonFieldMapping, LessonDto>> Projection
        {
            get
            {
                return x => new LessonDto
                {
                    LessonCode = x.Lesson.LessonCode,
                    LessonName = x.Lesson.LessonName,
                    ExamSamplesPrice = x.Lesson.ExamSamplesPrice,
                    Id = x.Id,
                    LessonId = x.Lesson.Id,
                    Exams = x.Lesson.ExamSamples.Select(e => new ExamSampleDto
                    {
                        Type = e.ExamSampleFile.Type,
                        FilePath = e.ExamSampleFile.FilePath,
                        SemesterTitle = e.ExamSampleFile.Semester.Title,
                    }).ToList(),
                    Courses = x.Lesson.Fields.SelectMany(f => f.TeacherLessonMappings.SelectMany(tl =>
                            tl.Courses.Where(c => c.ApprovalStatus == CourseApprovalStatus.ApprovedByAdminOrActivatedByTeacher)))
                        .Select(c =>
                            new CourseDto
                            {
                                Id = c.Id,
                                Price = c.Price,
                                Teacher = c.TeacherLessonMapping.Teacher.FullName,
                                Title = c.CourseTitle.Name,
                            }).ToList()
                };
            }
        }

        public static Expression<Func<Lesson, LessonExamDto>> ProjectionJustExams
        {
            get
            {
                return x => new LessonExamDto
                {
                    LessonCode = x.LessonCode,
                    LessonName = x.LessonName,
                    ExamSamplesPrice = x.ExamSamplesPrice,
                    Id = x.Id,
                    Exams = x.ExamSamples.Select(e => new ExamSampleDto
                    {
                        Type = e.ExamSampleFile.Type,
                        FilePath = e.ExamSampleFile.FilePath,
                        SemesterTitle = e.ExamSampleFile.Semester.Code,
                    }).ToList(),
                };
            }
        }
    }
}