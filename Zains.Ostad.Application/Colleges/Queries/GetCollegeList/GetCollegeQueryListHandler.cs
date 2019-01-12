using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zains.Ostad.Application.Colleges.Queries.GetCollegeList
{
    public class GetCollegeQueryHandler : IRequestHandler<GetCollegeQuery, List<CollegeListViewModel>>
    {
        private readonly IRepository<College, int> _collegeRepository;
        private readonly IMapper _mapper;

        public GetCollegeQueryHandler(IRepository<College, int> collegeRepository, IMapper mapper)
        {
            _collegeRepository = collegeRepository;
            _mapper = mapper;
        }

        public Task<List<CollegeListViewModel>> Handle(GetCollegeQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _collegeRepository.GetQueriable();
            if (request.GradeId.HasValue)
                dbQuery = dbQuery.Where(x => x.Fields.Any(f => f.GradeId == request.GradeId));
            if (!string.IsNullOrEmpty(request.SearchText))
                dbQuery = dbQuery.Where(x => x.Name.Contains(request.SearchText));
            switch (request.ProductType)
            {
                case ProductType.LessonExam:
                    dbQuery = dbQuery.Where(x => x.Fields.Any(f => f.FieldLessons.Any(l => l.Lesson.ExamSamples.Any())));
                    break;
                case ProductType.TeacherCourse:
                    dbQuery = dbQuery.Where(x =>
                        x.Fields.Any(f => f.FieldLessons.Any(s=>s.GradeId==request.GradeId && s.Lesson.Fields.Any(fl=>fl.TeacherLessonMappings.Any(tl => tl.Courses.Any())))));
                    break;
            }

            return dbQuery.ProjectTo<CollegeListViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}