using System;
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
        
        public static Func<EditAssignment,EditAssignmentViewModel> Projection => x => new EditAssignmentViewModel()
        {
            Status = x.Status,
            FilePath = x.FilePath,
            CreatedOn = x.CreatedOn,
            CourseItemId = x.CourseItemId,
            Title = x.CourseItem.Title,
            CourseItemFilePath = x.CourseItem.FilePath
        }; 
               
    }
}