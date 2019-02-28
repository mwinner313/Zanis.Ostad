using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.ExamSamples.Dto;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Lessons.Queries.GetLesson;
using Zains.Ostad.Application.Users.Dto;

namespace Zains.Ostad.Application.Users.Queries.GetBoughtLessonExamSamples
{
    public class GetBoughtLessonExamSamplesQuery:Pagenation,IRequest<PagenatedList<LessonExamDto>>
    {
    
    }
}