using System;
using System.Collections.Generic;
using Zanis.Ostad.Core.Entities.Edits;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.Contents
{
    public class CourseItem : BaseEntity<long>
    {
        public string Title { get; set; }
        public string FilePath { get; set; }
        public ContentType ContentType { get; set; }
        public CourseItemApprovalState State { get; set; }
        public bool IsPreview { get; set; }
        public Course Course { get; set; }
        public long CourseId { get; set; }
        public int Order { get; set; }
        public string AdminMessageForTeacher { get; set; }
        public string TeacherMessageForAdmin { get; set; }
        public DateTime CreatedOn { get; } = DateTime.Now;
        public EditStatus? LatestEditStatus { get; set; }
        public ICollection<EditAssignment> Edits { get; set; }
    }
}