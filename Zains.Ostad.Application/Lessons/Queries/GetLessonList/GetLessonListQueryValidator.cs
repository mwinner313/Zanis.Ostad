using FluentValidation;
using Swashbuckle.AspNetCore.Swagger;

namespace Zains.Ostad.Application.Lessons.Queries.GetLessonList
{
    public class GetLessonListQueryValidator:AbstractValidator<GetLessonListQuery>
    {
        public GetLessonListQueryValidator()
        {
            RuleFor(x => x.FieldId).NotNull().When(x => string.IsNullOrEmpty(x.SearchText));
            RuleFor(x => x.SearchText).MinimumLength(3);
        }
    }
}