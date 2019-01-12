using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;
using Xunit.Abstractions;
using Zanis.Ostad.Core.Entities;

namespace Zanis.Ostad.Repository.Tests
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void AddData()
        {
            var dataCtx = new DataDbContext();
            var ourContext = new ApplicationDbContext();
            var colleges = ourContext.Colleges.ToList();
            var grades = ourContext.Grades.ToList();
            dataCtx.Manbas.Where(x => x.UniGroupFieldCode.HasValue).GroupBy(x => new {x.UniGroupFieldCode, x.GradeCode})
                .ToList().ForEach(
                    groupingByFieldGrade =>
                    {
                        var collegeCode = groupingByFieldGrade.Key.UniGroupFieldCode.ToString().Substring(0, 2);
                        var collegeId = colleges
                            .FirstOrDefault(c => c.Code == collegeCode)?.Id;
                        if (!collegeId.HasValue)
                        {
                            return;
                        }

                        var gradeId = grades.First(c => c.Code == groupingByFieldGrade.Key.GradeCode.ToString()).Id;
                        var fieldCode = groupingByFieldGrade.Key.UniGroupFieldCode.ToString();
                        if (!ourContext.Fields.Any(f =>
                            f.Code == fieldCode && f.GradeId == gradeId))
                        {
                            var fieldName = dataCtx.Manbas.First(m =>
                                    m.UniGroupFieldCode == groupingByFieldGrade.Key.UniGroupFieldCode)
                                .Field;

                            var lessonsInFieldGrade = groupingByFieldGrade.Where(l => !string.IsNullOrEmpty(l.Lesson))
                                .Select(l =>
                                    new Lesson
                                    {
                                        LessonCode = l.UniGroupLessonCode.ToString(),
                                        LessonName = l.Lesson
                                    }).ToList();

                            AddLessonsInGrade(lessonsInFieldGrade, ourContext);

                            fieldName = fieldName.Substring(5);
                            ourContext.Fields.Add(new Field
                            {
                                Code = groupingByFieldGrade.Key.UniGroupFieldCode.ToString(),
                                CollegeId = collegeId.Value,
                                GradeId = gradeId,
                                Name = fieldName,
                                FieldLessons = GetFieldLessons(lessonsInFieldGrade, gradeId, ourContext)
                            });
                            ourContext.SaveChanges();
                        }
                    });
        }

        [Fact]
        public void ConnectFiledLesson()
        {
            var dataCtx = new DataDbContext();
            var ourContext = new ApplicationDbContext();
            var lessons = ourContext.Lessons.ToList();
            var grades = ourContext.Grades.ToList();
            var fields = ourContext.Fields.ToList();
            dataCtx.Manbas.Where(x => x.UniGroupFieldCode.HasValue).GroupBy(x => new {x.UniGroupFieldCode, x.GradeCode})
                .Select(x => new
                {
                    x.Key,
                    LessonCodes = x.Where(l => l.UniGroupFieldCode.HasValue).Select(r => r.UniGroupLessonCode).ToList()
                }).ToList().ForEach(
                    field =>
                    {
                        foreach (var lessonCode in field.LessonCodes)
                        {
                            if (lessonCode is null || !lessonCode.HasValue)
                                continue;
                            try
                            {
                                var gradeId = grades.FirstOrDefault(x => x.Code == field.Key.GradeCode.Value.ToString())
                                    .Id;
                                var lessonId = lessons.FirstOrDefault(x => x.LessonCode == lessonCode.Value.ToString())
                                    ?.Id;
                                if (!lessonId.HasValue)
                                {
                                    _testOutputHelper.WriteLine(lessonCode.Value.ToString());
                                    continue;
                                }

                                var fieldId = fields.FirstOrDefault(x =>
                                    x.Code == field.Key.UniGroupFieldCode.Value.ToString()).Id;
                                var lessonFieldMapping = new LessonFieldMapping
                                {
                                    LessonId = lessonId.Value,
                                    GradeId = gradeId,
                                    FieldId = fieldId,
                                };

                                if (!ourContext.LessonFieldMappings.Any(x =>
                                    x.FieldId == lessonFieldMapping.FieldId &&
                                    x.GradeId == lessonFieldMapping.GradeId &&
                                    x.LessonId == lessonFieldMapping.LessonId))
                                    ourContext.Add(lessonFieldMapping);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }
                        }

                        ourContext.SaveChanges();
                    });
        }

        private List<LessonFieldMapping> GetFieldLessons(List<Lesson> lessonsInFieldGrade, int gradeId,
            ApplicationDbContext ourContext)
        {
            var lessonCodes = lessonsInFieldGrade.Select(x => x.LessonCode);
            return ourContext.Lessons.Where(x => lessonCodes.Contains(x.LessonCode))
                .Select(x => new LessonFieldMapping()
                {
                    LessonId = x.Id,
                    GradeId = gradeId,
                }).ToList();
        }


        private static void AddLessonsInGrade(List<Lesson> lessonsInGrade, ApplicationDbContext ourContext)
        {
            foreach (var lesson in lessonsInGrade)
            {
                if (!ourContext.Lessons.Any(l => l.LessonCode == lesson.LessonCode))
                {
                    ourContext.Lessons.Add(lesson);
                }
            }

            ourContext.SaveChanges();
        }

        [Fact]
        public void AddExams()
        {
            var dbContext = new ApplicationDbContext();
            var semesters = dbContext.Semesters.ToList();
            foreach (var semester in semesters)
            {
                var dir = new DirectoryInfo(
                    $@"E:\WorkAndLife\Projects\Zanis.Ostad\Zanis.Ostad.Web\wwwroot\Files\{semester.Code}\soal");
                foreach (var fileInfo in dir.GetFiles())
                {
                    var matches = Regex.Matches(fileInfo.Name, "[0-9]+");
                   var lessonCodes =
                        matches.Select(x=>x.Value).Where(x=>x.Length==7).ToList();
                    if (dbContext.ExamSampleFiles.Any(x => fileInfo.FullName.Replace("\\", "/").Contains(x.FilePath)))
                        continue;
                    var exam = new ExamSampleFile
                    {
                        Type = ExamSampleFileType.Exam,
                        SemesterId = semester.Id,
                        FilePath = $"/Files/{semester.Code}/soal/" + fileInfo.Name
                    };
                    if (dbContext.ExamSampleFiles.Any(x => x.FilePath == exam.FilePath))
                        continue;
                    dbContext.ExamSampleFiles.Add(exam);
                    dbContext.SaveChanges();
                    var lessons = dbContext.Lessons.Where(x => lessonCodes.Contains(x.LessonCode)).ToList();
                    foreach (var lesson in lessons)
                    {
                        dbContext.LessonExamMappings.Add(new LessonExamMapping
                        {
                            LessonId = lesson.Id,
                            ExamSampleFileId = exam.Id
                        });
                    }
                }

                dbContext.SaveChanges();
            }
        }

        [Fact]
        public void AddAnatomicalAnswers()
        {
            var dbContext = new ApplicationDbContext();
            dbContext.Database.Migrate();
            var semesters = dbContext.Semesters.ToList();
            foreach (var semester in semesters)
            {
                var dir = new DirectoryInfo(
                    $@"E:\WorkAndLife\Projects\Zanis.Ostad\Zanis.Ostad.Web\wwwroot\Files\{semester.Code}\tashrihi");
                foreach (var fileInfo in dir.GetFiles())
                {
                    var matches = Regex.Matches(fileInfo.Name, "[0-9]+");
                    var lessonCodes =
                        matches.Select(x=>x.Value).Where(x=>x.Length==7).ToList();
                    if (dbContext.ExamSampleFiles.Any(x =>
                        fileInfo.FullName.ToLower().Replace("\\", "/").Replace("p", "").Contains(x.FilePath)))
                        continue;
                    var exam = new ExamSampleFile()
                    {
                        SemesterId = semester.Id,
                        FilePath = $"/Files/{semester.Code}/tashrihi/" + fileInfo.Name,
                        Type = ExamSampleFileType.AnatomicalAnswer
                    };
                    if (dbContext.ExamSampleFiles.Any(x => fileInfo.FullName.Replace("\\", "/").Contains(x.FilePath)))
                        continue;
                    dbContext.ExamSampleFiles.Add(exam);
                    dbContext.SaveChanges();
                    var lessons = dbContext.Lessons.Where(x => lessonCodes.Contains(x.LessonCode)).ToList();
                    foreach (var lesson in lessons)
                    {
                        dbContext.LessonExamMappings.Add(new LessonExamMapping()
                        {
                            LessonId = lesson.Id,
                            ExamSampleFileId = exam.Id
                        });
                    }
                }

                dbContext.SaveChanges();
            }
        }

       

        [Fact]
        public void AddTestsAnswers()
        {
            var dbContext = new ApplicationDbContext();
            var semesters = dbContext.Semesters.ToList();
            foreach (var semester in semesters)
            {
                var dir = new DirectoryInfo(
                    $@"E:\WorkAndLife\Projects\Zanis.Ostad\Zanis.Ostad.Web\wwwroot\Files\{semester.Code}\test");
                foreach (var fileInfo in dir.GetFiles())
                {
                    var lessonCodes = fileInfo.Name.Replace(fileInfo.Extension, "").Split("-").ToList();
                    if (dbContext.ExamSampleFiles.Any(x =>
                        fileInfo.FullName.ToLower().Replace("\\", "/").Replace("p", "").Contains(x.FilePath)))
                        continue;
                    var exam = new ExamSampleFile
                    {
                        SemesterId = semester.Id,
                        FilePath = $"/Files/{semester.Code}/test/" + fileInfo.Name,
                        Type = ExamSampleFileType.TestAnswer
                    };
                    if (dbContext.ExamSampleFiles.Any(x => fileInfo.FullName.Replace("\\", "/").Contains(x.FilePath)))
                        continue;
                    dbContext.ExamSampleFiles.Add(exam);
                    dbContext.SaveChanges();
                    var lessons = dbContext.Lessons.Where(x => lessonCodes.Contains(x.LessonCode)).ToList();
                    foreach (var lesson in lessons)
                    {
                        dbContext.LessonExamMappings.Add(new LessonExamMapping()
                        {
                            LessonId = lesson.Id,
                            ExamSampleFileId = exam.Id
                        });
                    }
                }
                dbContext.SaveChanges();
            }
        }

        class Dummy
        {
            public bool IsDeleted { get; set; }
        }

        [Fact]
        public void RemoveFileDuplicates()
        {
            var dir = new DirectoryInfo(@"E:\WorkAndLife\Projects\Zanis.Ostad\Zanis.Ostad.Web\wwwroot\Files\");
            RemoveDuplicateInDirectory(dir);
        }

        private static void RemoveDuplicateInDirectory(DirectoryInfo dir)
        {
            var list = new Dictionary<FileInfo, Dummy>();
            dir.GetFiles().OrderByDescending(x => x.Name.Length).ToList().ForEach(x => list.Add(x, new Dummy()
            {
                IsDeleted = false
            }));
            foreach (var fileInfo in list)
            {
                if (fileInfo.Value.IsDeleted)
                    continue;
                var matches = Regex.Matches(fileInfo.Key.Name, "[0-9]+");
                var fileInfoLessonCodes = matches.Select(x=>x.Value).Where(x=>x.Length==7).ToList();
                var innerList = dir.GetFiles().OrderByDescending(x => x.Name.Length).ToList();
                foreach (var innerFile in innerList)
                {
                    if (fileInfo.Key.Name == innerFile.Name)
                        continue;
                    var matches2 = Regex.Matches(innerFile.Name, "[0-9]+");
                    var innerFileLessonCodes = matches2.Select(x=>x.Value).Where(x=>x.Length==7).ToList();
                    if (fileInfoLessonCodes.Any(x => innerFileLessonCodes.Any(y => y == x)))
                    {
                        if (innerFileLessonCodes.Count > fileInfoLessonCodes.Count)
                        {
                            fileInfo.Key.Delete();
                            break;
                        }

                        innerFile.Delete();
                        list[list.First(x => x.Key.Name == innerFile.Name).Key].IsDeleted = true;
                    }
                }
            }

            foreach (var directoryInfo in dir.GetDirectories())
            {
                RemoveDuplicateInDirectory(directoryInfo);
            }
        }
    }
}