using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Lessons.Queries.GetLesson;
using Zains.Ostad.Application.Users.Dto;

namespace Zains.Ostad.Application.Users.Queries.GetBoughtCourses
{
    public class GetBoughtCoursesQuery:Pagenation,IRequest<List<UserCourseDto>>
    {
    }
}