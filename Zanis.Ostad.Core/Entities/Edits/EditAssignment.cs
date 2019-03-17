using System;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.Edits
{
    public class EditAssignment : BaseEntity<long>
    {
        public CourseItem CourseItem { get; set; }
        public User Editor { get; set; }
        public long EditorId { get; set; }
        public long CourseItemId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public EditStatus Status { get; set; }
        public string FilePath { get; set; }
    }
}