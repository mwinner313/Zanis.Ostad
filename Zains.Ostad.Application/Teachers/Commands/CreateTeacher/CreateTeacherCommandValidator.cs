using FluentValidation;

namespace Zains.Ostad.Application.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommandValidator:AbstractValidator<CreateTeacherCommand>
    {
        public CreateTeacherCommandValidator()
        {
            RuleFor(x=>x.FullName).NotEmpty();
            RuleFor(x=>x.FullName).NotNull();
            RuleFor(x=>x.FullName).MinimumLength(5);
            RuleFor(x=>x.TeacherCode).NotEmpty();
            RuleFor(x=>x.TeacherCode).NotNull();
            RuleFor(x=>x.TeacherCode).MinimumLength(5);
        }
    }
}