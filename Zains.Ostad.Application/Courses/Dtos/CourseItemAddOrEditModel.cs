using Microsoft.AspNetCore.Http;

namespace Zains.Ostad.Application.Courses.Dtos
{
    public class CourseItemAddOrEditModel
    {
      
        public string Title { get; set; }
        public bool IsPreview { get; set; }
        public int Order { get; set; }
    }
}