using MediatR;
using Microsoft.AspNetCore.Http;
using Zains.Ostad.Application.Courses.Dtos;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses.Commands.AddCourseItem
{
    public class EditCourseItemCommand : IRequest<CourseItemViewModel>
    {
        public long Id { get; set; }
        public bool IsPreview { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public IFormFile File { get; set; }
        public CourseItemApprovalState State { get; set; }
    }
}