using System.Collections.Generic;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.Contents
{
    public class CourseTitle:BaseEntity<int>
    {
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
        public string Description { get; set; }
    }
}