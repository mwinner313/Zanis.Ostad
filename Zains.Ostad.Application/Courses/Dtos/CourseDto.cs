using System;
using System.Collections.Generic;
using Zains.Ostad.Application.Lessons.Queries.GetLessonList;
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
        public CourseApprovalStatus ApprovalStatus { get; set; }
        public string PreviewFilePath { get; set; }
        public List<LessonFieldViewModel> RelatedLessonFields { get; set; }
        public List<CourseItemViewModel> Contents { get; set; }
        public bool IsOwnedByCurrentUser { get; set; }
        public long Id { get; set; }
        public string Description { get; set; }
        public string TeacherAvatar { get; set; }
        public bool HasPendingItemToApprove { get; set; }
        public long TeacherId { get; set; }
        public string TeacherMessageForAdmin { get; set; }
        public string AdminMessageForTeacher { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LessonTitle { get; set; }
        public string FieldName { get; set; }
        public string ImagePath { get; set; }
        public TimeSpan Duration { get; set; }
    }
}