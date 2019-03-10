using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zanis.Ostad.Core.Contracts
{
    public interface ICoursesFileManager
    {
        Task SaveFile(IFormFile file, TeacherLessonMapping teacherLessonMapping);
        Task SaveFile(IFormFile requestFile, long courseId);
        void DeleteFile(string filePath);
        Task<string> GetFilePathForDownload(IFormFile requestFile, long courseId);
        string GetFilePathForDownload(IFormFile requestZipFile, TeacherLessonMapping teacherLessonMapping);
    }
}