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
        public ICollection<CourseItem> Contents { get; set; } = new List<CourseItem>();
        public ICollection<CourseLessonFieldGradeMapping> Lessons { get; set; } = new List<CourseLessonFieldGradeMapping>();
        public CourseCategory CourseCategory { get; set; }
        public int CourseCategoryId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public CourseApprovalStatus ApprovalStatus { get; set; }
        public ICollection<StudentCourseMapping> Students { get; set; }
        public bool HasPendingItemToApprove { get; set; }
        public long TeacherId { get; set; }
        public User Teacher { get; set; }
        public string ImagePath { get; set; }
        public TimeSpan Duration { get; set; }
        public string Title { get; set; }
    }
}