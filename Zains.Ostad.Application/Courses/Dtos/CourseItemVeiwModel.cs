using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses.Dtos
{
    public class CourseItemVeiwModel
    {
        
        public string Title { get; set; }
        public string FilePath { get; set; }
        public ContentType ContentType { get; set; }
        public long CourseId { get; set; }
        public bool IsPreview { get; set; }
        public long Id { get; set; }
    }
}