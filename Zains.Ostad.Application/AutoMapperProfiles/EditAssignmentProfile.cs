using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zains.Ostad.Application.Editors.Queries.GetEditAssignments;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.AutoMapperProfiles
{
    public class EditAssignmentProfile:Profile
    {
        public EditAssignmentProfile()
        {
            CreateMap<EditAssignment, EditAssignmentViewModel>();
        }
        
        public static Func<EditAssignment,EditAssignmentViewModel> Projection => x => new EditAssignmentViewModel
        {
            Status = x.Status,
            FilePath = x.FilePath,
            Id=x.Id,
            CreatedOn = x.CreatedOn,
            CourseItemId = x.CourseItemId,
            Title = $"{x.CourseItem?.Title} - {x.CourseItem?.Course?.CourseTitle?.Name} - {x.CourseItem?.Course?.Lessons?.FirstOrDefault()?.Lesson.Lesson.LessonCode} - {x.CourseItem?.Course?.Lessons?.FirstOrDefault()?.Lesson.Field.Name} - {x.CourseItem?.Course?.Lessons?.FirstOrDefault()?.Lesson.Grade?.Name} - {x.CourseItem?.Course?.Teacher.FullName}",
            CourseItemFilePath = x.CourseItem?.FilePath,
            EditorFullName = x.Editor?.FullName
        }; 
               
    }
}