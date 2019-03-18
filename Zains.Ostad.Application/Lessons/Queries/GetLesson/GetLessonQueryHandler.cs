using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.AutoMapperProfiles;
using Zains.Ostad.Application.Lessons.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.Lessons.Queries.GetLesson
{
    public class GetLessonQueryHandler : IRequestHandler<GetLessonQuery, LessonDto>
    {
        private readonly IRepository<LessonFieldMapping, long> _lessonRepository;
        private readonly IRepository<StudentExamSampleMapping, long> _studentLessonExamMappingRepo;

        public GetLessonQueryHandler(IRepository<LessonFieldMapping, long> lessonRepository,
            IRepository<StudentExamSampleMapping, long> studentLessonExamMappingRepo)
        {
            _lessonRepository = lessonRepository;
            _studentLessonExamMappingRepo = studentLessonExamMappingRepo;
        }

        public async Task<LessonDto> Handle(GetLessonQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetQueryable().Select(LessonProfile.Projection)
                .FirstAsync(x => x.Id == request.LessonId, cancellationToken);
            lesson.IsOwnedByCurrentUser = _studentLessonExamMappingRepo.GetQueryable()
                .Any(x => x.StudentId == request.RequestingUserId && x.LessonId == lesson.Id);
            return lesson;
        }
    }
}