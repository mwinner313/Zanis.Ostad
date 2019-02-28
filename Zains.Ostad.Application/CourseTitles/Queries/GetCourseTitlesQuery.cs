using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.CourseTitles.Dtos;

namespace Zains.Ostad.Application.CourseTitles.Queries
{
    public class GetCourseTitlesQuery:IRequest<List<CourseTitleViewModel>>
    {
        
    }
}