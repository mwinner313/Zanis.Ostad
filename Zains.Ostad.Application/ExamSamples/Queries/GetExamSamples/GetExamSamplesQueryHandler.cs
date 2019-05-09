//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using MediatR;
//using Microsoft.EntityFrameworkCore.Internal;
//using Zains.Ostad.Application.ExamSamples.Dto;
//using Zanis.Ostad.Core.Contracts;
//using Zanis.Ostad.Core.Entities;
//
//namespace Zains.Ostad.Application.ExamSamples.Queries.GetExamSamples
//{
//    public class GetExamSamplesQueryHandler:IRequestHandler<GetExamSamplesQuery,List<ExamSampleDto>>
//    {
//        private readonly IRepository<ExamSampleFile, int> _repository;
//
//        public GetExamSamplesQueryHandler(IRepository<ExamSampleFile, int> repository)
//        {
//            _repository = repository;
//        }
//
//        public Task<List<ExamSampleDto>> Handle(GetExamSamplesQuery request, CancellationToken cancellationToken)
//        {
//           var examSamples = _repository.GetQueryable().Where(x => x.Lessons.Any(l =>
//                l.Lesson
//        }
//    }
//}