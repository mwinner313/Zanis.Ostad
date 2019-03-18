using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.ExamSamples.Queries.HasAccessToFile
{
    public class
        HasAccessToFileQueryHandler : IRequestHandler<HasAccessToFileQuery, Response
        >
    {
        private readonly IRepository<StudentExamSampleMapping, long> _studentBoughtExamRepository;
        private readonly IRepository<ExamSampleFile, int> _examRepository;

        public HasAccessToFileQueryHandler(IRepository<StudentExamSampleMapping, long> studentBoughtExamRepository, IRepository<ExamSampleFile, int> examRepository)
        {
            _studentBoughtExamRepository = studentBoughtExamRepository;
            _examRepository = examRepository;
        }

        public async Task<Response> Handle(HasAccessToFileQuery request,
            CancellationToken cancellationToken)
        {
            if (!ExamFileExists(request)) 
                return Response.UnKnown();
            
            return DoesUserBoughtThisItem(request)
                ? Response.Success()
                : Response.Failed();
        }

        private bool ExamFileExists(HasAccessToFileQuery request)
        {
            return _examRepository.GetQueryable().Any(x => request.FilePath.Contains(x.FilePath));
        }

        private bool DoesUserBoughtThisItem(HasAccessToFileQuery request)
        {
            return _studentBoughtExamRepository.GetQueryable().Any(x =>
                x.Lesson.ExamSamples.Any(e => request.FilePath.Contains(e.ExamSampleFile.FilePath) && x.StudentId == request.UserId));
        }
    }
}