using FluentValidation;

namespace Zains.Ostad.Application.Teachers.Commands.AddEditCourse
{
    public class AddCourseCommandValidator:AbstractValidator<AddCourseCommand>
    {
        public AddCourseCommandValidator()
        {
            RuleFor(x => x.Price).NotEqual(0);
            RuleFor(x => x.CourseTitleId).NotEqual(0);
            RuleFor(x => x.LessonFieldIds).NotNull();
        }
    }
}