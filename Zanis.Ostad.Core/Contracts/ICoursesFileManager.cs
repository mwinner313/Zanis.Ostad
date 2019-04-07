using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zanis.Ostad.Core.Contracts
{
    public interface ICoursesFileManager
    {
        Task SaveFile(IFormFile file, string teacherUserName, long courseId);
        Task SaveEditedFileFileByEditor(IFormFile file, string teacherUserName, long courseId);
        Task<string> GetEditedFilePathForDownload(IFormFile file, string teacherUserName, long courseId);
        string GetFilePathForDownload(IFormFile requestZipFile, string teacherUserName, long courseId);
        void DeleteFile(string filePath);
    }
}