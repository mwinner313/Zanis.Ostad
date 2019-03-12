using System;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses.Dtos
{
    public class CourseItemViewModel
    {
        public string Title { get; set; }
        public string TeacherMessageForAdmin { get; set; }
        public string AdminMessageForTeacher { get; set; }
        public string FilePath { get; set; }
        public ContentType ContentType { get; set; }
        public CourseItemApprovalState State { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Order { get; set; }
        public long CourseId { get; set; }
        public bool IsPreview { get; set; }
        public long Id { get; set; }
    }
}