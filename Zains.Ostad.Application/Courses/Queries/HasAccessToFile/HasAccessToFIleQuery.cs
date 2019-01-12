using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Courses.Queries.HasAccessToFile
{
    public class HasAccessToFileQuery:IRequest<Response>
    {
        public long UserId { get; }
        public string FilePath { get; set; }
        public HasAccessToFileQuery(long userId, string pathValue)
        {
            UserId = userId;
            FilePath = pathValue;
        }
    }
}