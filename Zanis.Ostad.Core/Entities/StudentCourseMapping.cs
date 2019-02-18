using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities
{
    public class StudentCourseMapping:BaseEntity<long>
    {
        public User Student { get; set; }
        public long StudentId { get; set; }
        public Course Course { get; set; }
        public long CourseId { get; set; }
    }
}