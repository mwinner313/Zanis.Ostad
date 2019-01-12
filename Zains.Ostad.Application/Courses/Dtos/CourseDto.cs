using System.Collections.Generic;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses.Dtos
{
    public class CourseDto
    {
        public string Teacher { get; set; }
        public int Price { get; set; }
        public int PriceAsTomans => Price / 10;
        public string Title { get; set; }
        public ContentType PreviewType { get; set; }
        public string PreviewFilePath { get; set; }
        public string GradeTitle { get; set; }
        public List<string> RelatedFields { get; set; }
        public List<CourseItemDto> Contents { get; set; }
        public bool IsOwnedByCurrentUser { get; set; }
        public long Id { get; set; }
        public string LessonCode { get; set; }
        public string Description { get; set; }
        public string TeacherAvatar { get; set; }
    }
}