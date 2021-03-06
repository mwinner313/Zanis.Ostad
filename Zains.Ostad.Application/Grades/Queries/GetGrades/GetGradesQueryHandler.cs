using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.Grades.Queries.GetGrades
{
    public class GetGradesQueryHandler:IRequestHandler<GetGradesQuery,List<GradeViewModel>>,
        IRequestHandler<GetGradeQuery,GradeViewModel>

    {
        private readonly IRepository<Grade, int> _gradesRepository;
        private readonly IMapper _mapper;
        public GetGradesQueryHandler(IRepository<Grade, int> gradesRepository, IMapper mapper)
        {
            _gradesRepository = gradesRepository;
            _mapper = mapper;
        }

        public Task<List<GradeViewModel>> Handle(GetGradesQuery request, CancellationToken cancellationToken)
        {
            return _gradesRepository.GetQueryable().Where(x=>x.IsActive).ProjectTo<GradeViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<GradeViewModel> Handle(GetGradeQuery request, CancellationToken cancellationToken)
        {
            var grade =await _gradesRepository.GetById(request.GradeId);
            return _mapper.Map<GradeViewModel>(grade);
        }
    }
}