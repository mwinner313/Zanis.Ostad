using System;
using System.Collections.Generic;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.Contents
{
    public class Course : BaseEntity<long>
    {
        public int Price { get; set; }
        public string Description { get; set; }
        public string AdminMessageForTeacher { get; set; }
        public string TeacherMessageForAdmin { get; set; }
        public ICollection<CourseItem> Contents { get; set; }
        public TeacherLessonMapping TeacherLessonMapping { get; set; }
        public long TeacherLessonMappingId { get; set; }
        public CourseTitle CourseTitle { get; set; }
        public int CourseTitleId { get; set; }
        public DateTime CreatedOn { get; } = DateTime.Now;
        public CourseApprovalStatus ApprovalStatus { get; set; }
        public ICollection<StudentCourseMapping> Students { get; set; }
        public string ZipFilesPath { get; set; }
        public int PendingToApproveItemsCount { get; set; }
    }
}