using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zains.Ostad.Application.CourseTitles.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.CourseTitles.Queries
{
    public class GetCourseTitlesQueryHandler : IRequestHandler<GetCourseTitlesQuery, List<CourseTitleViewModel>>
    {
        private IRepository<CourseTitle, int> _repository;

        public GetCourseTitlesQueryHandler(IRepository<CourseTitle, int> repository)
        {
            _repository = repository;
        }

        public async Task<List<CourseTitleViewModel>> Handle(GetCourseTitlesQuery request,
            CancellationToken cancellationToken)
        {
            return _repository.GetQueriable().Select(x => new CourseTitleViewModel() {Id = x.Id, Name = x.Name})
                .ToList();
        }
    }
}