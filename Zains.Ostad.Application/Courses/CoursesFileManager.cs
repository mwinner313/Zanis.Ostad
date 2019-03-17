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

        private string CreatePathBySegment(string segment, TeacherLessonMapping teacherLessonMapping)
        {
            var gradeCode = teacherLessonMapping.LessonFieldMapping.Grade.Code;
            var fieldCode = teacherLessonMapping.LessonFieldMapping.Field.Code;
            var lessonCode = teacherLessonMapping.LessonFieldMapping.Lesson.LessonCode;
            var userName = teacherLessonMapping.Teacher.UserName;
            return $"{_hostingEnvironment.WebRootPath}/{segment}/{gradeCode}/{fieldCode}/{lessonCode}/{userName}";
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

        public async Task SaveFile(IFormFile file, string path)
        {
            var dir = new FileInfo(path).Directory.FullName;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public async Task SaveFile(IFormFile file, TeacherLessonMapping teacherLessonMapping)
        {
            await SaveFile(file, GetFilePath(file, teacherLessonMapping));
        }

        private string GetFilePath(IFormFile file,
            TeacherLessonMapping teacherLessonMapping)
        {
            return $"{CreatePathBySegment("Courses", teacherLessonMapping)}/{file.FileName}";
        }


        public async Task SaveEditedFileFileByEditor(IFormFile file, long courseId)
        {
            await SaveFile(file,
                $"{CreatePathBySegment("Edits", await GetTeacherLessonMapping(courseId))}/{file.FileName}");
        }

        public async Task<string> GetEditedFilePathForDownload(IFormFile file, long courseId)
        {
            return $"{CreatePathBySegment("Edits", await GetTeacherLessonMapping(courseId))}/{file.FileName}".Replace(
                _hostingEnvironment.WebRootPath, "");
        }

        private async Task<string> GetFilePath(IFormFile file, long courseId)
        {
            return GetFilePath(file, await GetTeacherLessonMapping(courseId));
        }

        public async Task SaveFile(IFormFile file, long courseId)
        {
            await SaveFile(file, await GetTeacherLessonMapping(courseId));
        }

        public async Task<string> GetFilePathForDownload(IFormFile requestFile, long courseId)
        {
            return (await GetFilePath(requestFile, courseId)).Replace(_hostingEnvironment.WebRootPath, "");
        }

        public string GetFilePathForDownload(IFormFile requestZipFile, TeacherLessonMapping teacherLessonMapping)
        {
            return GetFilePath(requestZipFile, teacherLessonMapping).Replace(_hostingEnvironment.WebRootPath, "");
        }

        public void DeleteFile(string filePath)
        {
            File.Delete(_hostingEnvironment.WebRootPath + filePath);
        }
    }
}