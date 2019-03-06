using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses
{
    public class CoursesFileManager : ICoursesFileManager
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IRepository<TeacherLessonMapping, long> _teacherLessonMappingRepo;
        private TeacherLessonMapping _teacherLessonMapping;
        private long _courseId;

        public CoursesFileManager(IHostingEnvironment hostingEnvironment,
            IRepository<TeacherLessonMapping, long> teacherLessonMappingRepo)
        {
            _hostingEnvironment = hostingEnvironment;
            _teacherLessonMappingRepo = teacherLessonMappingRepo;
        }

        public async Task SaveFile(IFormFile file, TeacherLessonMapping teacherLessonMapping)
        {
            var filePath = GetFilePath(file, teacherLessonMapping);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                var dir = new FileInfo(filePath).Directory.FullName;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                await file.CopyToAsync(fileStream);
            }
        }

        public string GetFilePath(IFormFile file,
            TeacherLessonMapping teacherLessonMapping)
        {
            var gradeCode = teacherLessonMapping.LessonFieldMapping.Grade.Code;
            var fieldCode = teacherLessonMapping.LessonFieldMapping.Field.Code;
            var lessonCode = teacherLessonMapping.LessonFieldMapping.Lesson.LessonCode;
            var userName = teacherLessonMapping.Teacher.UserName;
            
            var dir =
                $"{_hostingEnvironment.ContentRootPath}/Courses/{gradeCode}/{fieldCode}/{lessonCode}/{userName}";
            
            return $"{dir}/{file.FileName}";
        }

        public async Task<string> GetFilePath(IFormFile file, long courseId)
        {
            return GetFilePath(file, await GetTeacherLessonMapping(courseId));
        }

        public async Task SaveFile(IFormFile file, long courseId)
        {
            await SaveFile(file, await GetTeacherLessonMapping(courseId));
        }

        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        private async Task<TeacherLessonMapping> GetTeacherLessonMapping(long courseId)
        {
            if (courseId == 0)
                throw new ArgumentException(nameof(courseId));

            if (_courseId != courseId)
            {
                _courseId = courseId;
                _teacherLessonMapping = _teacherLessonMappingRepo.GetQueriable()
                    .Include(x => x.Teacher)
                    .Include(x => x.LessonFieldMapping)
                    .Include(x => x.LessonFieldMapping.Field)
                    .Include(x => x.LessonFieldMapping.Lesson)
                    .Include(x => x.LessonFieldMapping.Grade)
                    .FirstOrDefault(x =>
                        x.Courses.Any(c => c.Id == courseId));
            }

            return _teacherLessonMapping;
        }
    }
}