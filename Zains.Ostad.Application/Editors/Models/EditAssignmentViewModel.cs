using System;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Editors.Queries.GetEditAssignments
{
    public class EditAssignmentViewModel
    {
        public long CourseItemId { get; set; }
        public string Title { get; set; }
        public string CourseItemFilePath { get; set; }
        public DateTime CreatedOn { get; set; } 
        public EditStatus Status { get; set; }
        public string FilePath { get; set; }
    }
}