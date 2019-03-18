using MediatR;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Courses.Queries.GetCourseItemsToEdit
{
    public class GetCourseItemsToEdit :Pagenation, IRequest<PagenatedList<CourseItemViewModel>>
    {
        public EditStatus? EditStatus { get; set; }
    }
}