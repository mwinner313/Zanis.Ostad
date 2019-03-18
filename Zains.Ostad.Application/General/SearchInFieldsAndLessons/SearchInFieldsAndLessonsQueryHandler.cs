using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.General.SearchInFieldsAndLessons
{
    public class  SearchInFieldsAndLessonsQueryHandler : IRequestHandler<SearchInFieldsAndLessonsQuery, List<SearchResult>>
    {
        private readonly IRepository<LessonFieldMapping, long> _lessonFieldRepository;
        private readonly IRepository<Field, int> _fieldRepository;

        public SearchInFieldsAndLessonsQueryHandler(IRepository<LessonFieldMapping, long> lessonFieldRepository,
            IRepository<Field, int> fieldRepository)
        {
            _lessonFieldRepository = lessonFieldRepository;
            _fieldRepository = fieldRepository;
        }

        public async Task<List<SearchResult>> Handle(SearchInFieldsAndLessonsQuery request,
            CancellationToken cancellationToken)
        {
            request.Term = request.Term.Trim();
            var list = new List<SearchResult>();
            if (!request.Type.HasValue || request.Type == SearchItemType.Field)
            {
                var fields = _fieldRepository.GetQueryable().Where(x => x.Name.Contains(request.Term)).Select(x =>
                    new SearchResult
                    {
                        Id = x.Id,
                        Type = SearchItemType.Field,
                        Title = x.Name,
                        Grade = x.Grade.Name
                    });
                list.AddRange(fields);
            }

            if (!request.Type.HasValue || request.Type == SearchItemType.Lesson)
            {
                var lessons = _lessonFieldRepository.GetQueryable()
                    .Where(x => x.Lesson.LessonName.Contains(request.Term)).GroupBy(x => new
                    {
                        x.Grade.Name,
                        x.Lesson.LessonCode
                    }).Select(x => new SearchResult
                    {
                        Id = x.First().Id,
                        Type = SearchItemType.Lesson,
                        Title = x.First().Lesson.LessonName,
                        Fields = x.Select(l => l.Field.Name).ToList(),
                        Grade = x.Key.Name
                    });
                list.AddRange(lessons);
            }
            return list;
        }
    }
}