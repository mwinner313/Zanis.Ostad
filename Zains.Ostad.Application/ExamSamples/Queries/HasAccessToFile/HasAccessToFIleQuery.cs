using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.ExamSamples.Queries.HasAccessToFile
{
    public class HasAccessToFileQuery:IRequest<Response>
    {
        public long UserId { get; }
        public string FilePath { get; }
        public HasAccessToFileQuery(long userId, string filePath)
        {
            UserId = userId;
            FilePath = filePath;
        }
    }
}