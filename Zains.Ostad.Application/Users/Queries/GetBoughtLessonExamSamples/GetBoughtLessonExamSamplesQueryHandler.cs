using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.AutoMapperProfiles;
using Zains.Ostad.Application.ExamSamples.Dto;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Lessons.Queries.GetLesson;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.Users.Queries.GetBoughtLessonExamSamples
{
    public class GetBoughtLessonExamSamplesQueryHandler:IRequestHandler<GetBoughtLessonExamSamplesQuery,List<LessonExamDto>>
    {
        private readonly IRepository<StudentExamSampleMapping,long> _repository;
        private readonly IWorkContext _workContext;
        public GetBoughtLessonExamSamplesQueryHandler(IRepository<StudentExamSampleMapping, long> repository, IMapper mapper, IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        public Task<List<LessonExamDto>> Handle(GetBoughtLessonExamSamplesQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetQueriable()
                .Where(x => x.StudentId == _workContext.CurrentUserId)
                .OrderByDescending(x => x.Id)
                .Pagenate(request)
                .Select(x => x.Lesson).Select(LessonProfile.ProjectionJustExams)
                .ToListAsync(cancellationToken);
        }
    }
}