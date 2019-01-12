using System.Collections;
using System.Collections.Generic;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities
{
    public class Field: BaseEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public College College { get; set; }
        public Grade Grade { get; set; }
        public int GradeId { get; set; }
        public int CollegeId { get; set; }
        
        public ICollection<LessonFieldMapping> FieldLessons { get; set; }
    }
}