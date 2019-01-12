using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.Contents
{
    public class CourseItem:BaseEntity<long>
    {
        public string Title { get; set; }
        public string FilePath { get; set; }
        public ContentType ContentType { get; set; }
        public bool IsPreview { get; set; }
        public Course Course { get; set; }
        public long CourseId { get; set; }
    }
}