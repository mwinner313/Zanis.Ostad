using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses
{
    public class CoursesFileManager : ICoursesFileManager
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public CoursesFileManager(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        private string CreatePathBySegment(string segment, string teacherUserName,long courseId)
        {
            return $"{_hostingEnvironment.WebRootPath}/{segment}/{teacherUserName}/{courseId}";
        }
      

        private async Task SaveFile(IFormFile file, string path)
        {
            var dir = new FileInfo(path).Directory.FullName;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public async Task SaveFile(IFormFile file,string teacherUserName,long courseId)
        {
            await SaveFile(file, GetFilePath(file, teacherUserName,courseId));
        }

        private string GetFilePath(IFormFile file,string teacherUserName,long courseId)
        {
            return $"{CreatePathBySegment("Courses",teacherUserName,courseId)}/{file.FileName}";
        }


        public async Task SaveEditedFileFileByEditor(IFormFile file,string teacherUserName, long courseId)
        {
            await SaveFile(file,
                $"{CreatePathBySegment("Edits", teacherUserName,courseId)}/{file.FileName}");
        }

        public async Task<string> GetEditedFilePathForDownload(IFormFile file,string teacherUserName, long courseId)
        {
            return $"{CreatePathBySegment("Edits", teacherUserName,courseId)}/{file.FileName}".Replace(
                _hostingEnvironment.WebRootPath, "");
        }

        public string GetFilePathForDownload(IFormFile requestZipFile, string teacherUserName,long courseId)
        {
            return GetFilePath(requestZipFile, teacherUserName,courseId).Replace(_hostingEnvironment.WebRootPath, "");
        }

        public void DeleteFile(string filePath)
        {
            File.Delete(_hostingEnvironment.WebRootPath + filePath);
        }
    }
}