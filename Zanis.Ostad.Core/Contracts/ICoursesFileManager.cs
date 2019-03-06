using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zanis.Ostad.Core.Contracts
{
    public interface ICoursesFileManager
    {
        Task SaveFile(IFormFile file, TeacherLessonMapping teacherLessonMapping);
        string GetFilePath(IFormFile file, TeacherLessonMapping teacherLessonMapping);
        Task<string> GetFilePath(IFormFile file, long courseId);
        Task SaveFile(IFormFile requestFile, long courseId);
        void DeleteFile(string filePath);
    }
}