using Microsoft.AspNetCore.Http;

namespace Zains.Ostad.Application.Courses.Dtos
{
    public class CourseItemDto
    {
        public IFormFile File { get; set; }
        public string Title { get; set; }
        public bool IsPreview { get; set; }
        public int Order { get; set; }
    }
}