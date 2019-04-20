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

namespace Zains.Ostad.Application.Fields.Queries.GetFieldsList
{
    public class GetFieldsListQueryHandler : IRequestHandler<GetFieldsListQuery, List<FieldListViewModel>>
    ,IRequestHandler<GetFieldQuery,FieldListViewModel>
    {
        private readonly IRepository<Field, int> _fieldRepositoy;
        private readonly IMapper _mapper;

        public GetFieldsListQueryHandler(IRepository<Field, int> fieldRepositoy, IMapper mapper)
        {
            _fieldRepositoy = fieldRepositoy;
            _mapper = mapper;
        }

        public Task<List<FieldListViewModel>> Handle(GetFieldsListQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _fieldRepositoy.GetQueryable();
            if (request.GradeId.HasValue)
                dbQuery = dbQuery.Where(x => x.GradeId == request.GradeId);
            if (request.CollegeId.HasValue)
                dbQuery = dbQuery.Where(x => x.CollegeId == request.CollegeId);
            if (!string.IsNullOrEmpty(request.SearchText))
                dbQuery = dbQuery.Where(x => x.Name.Contains(request.SearchText));

            switch (request.ProductType)
            {
                case ProductType.LessonExam:
                    dbQuery = dbQuery.Where(f => f.FieldLessons.Any(l => l.Lesson.Fields.Any(x=>x.Lesson.ExamSamples.Any())));
                    break;
                case ProductType.TeacherCourse:
                    dbQuery = dbQuery.Where(
                        f => f.FieldLessons.Any(l => l.Lesson.Fields.Any(x=>x.Courses.Any())));
                    break;
            }

            return dbQuery.ProjectTo<FieldListViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<FieldListViewModel> Handle(GetFieldQuery request, CancellationToken cancellationToken)
        {
            var field = _fieldRepositoy.GetQueryable().First(x => x.Id == request.FieldId);
            return _mapper.Map<FieldListViewModel>(field);
        }
    }
}