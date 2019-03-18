using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Zains.Ostad.Application.AutoMapperProfiles;
using Zains.Ostad.Application.Lessons.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zains.Ostad.Application.Lessons.Queries.GetLessonList
{
    public class GetLessonListQueryHandler : IRequestHandler<GetLessonListQuery, List<LessonFieldListViewModel>>,IRequestHandler<GetLessonListWithDetailsQuery,List<LessonFieldViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<LessonFieldMapping, long> _lessonRepository;

        public GetLessonListQueryHandler(IMapper mapper, IRepository<LessonFieldMapping, long> lessonRepository)
        {
            _mapper = mapper;
            _lessonRepository = lessonRepository;
        }

        public Task<List<LessonFieldListViewModel>> Handle(GetLessonListQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _lessonRepository.GetQueryable();
            if (request.FieldId.HasValue)
                dbQuery = dbQuery.Where(x => x.FieldId == request.FieldId);
            if (!string.IsNullOrEmpty(request.SearchText))
                dbQuery = dbQuery.Where(x => x.Lesson.LessonName.Contains(request.SearchText));
            switch (request.ProductType)
            {
                case ProductType.LessonExam:
                    dbQuery = dbQuery.Where(x => x.Lesson.ExamSamples.Any());
                    break;
                case ProductType.TeacherCourse:
                    dbQuery = dbQuery.Where(x => x.TeacherLessonMappings.Any(tl => tl.Courses.Any()));
                    break;
            }

            return dbQuery.Select(LessonProfile.ProjectionList)
                .ToListAsync();
        }

        public Task<List<LessonFieldViewModel>> Handle(GetLessonListWithDetailsQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _lessonRepository.GetQueryable().Where(x=>x.Lesson.LessonName.Contains(request.Term)||x.Lesson.LessonCode.Contains(request.Term))
                .Select(x=>new LessonFieldViewModel
                {
                    Id = x.Id,
                    LessonId = x.LessonId,
                    LessonName = x.Lesson.LessonName,
                    FieldId = x.Field.Id,
                    FieldName = x.Field.Name,
                    GradeId = x.GradeId,
                    GradeName = x.Grade.Name
                });
            
            if (request.FieldId.HasValue)
                dbQuery = dbQuery.Where(x => x.FieldId == request.FieldId);
            
            if (request.GradeId.HasValue)
                dbQuery = dbQuery.Where(x => x.GradeId == request.GradeId);
            
            return dbQuery.ToListAsync(cancellationToken);
        }
    }
}