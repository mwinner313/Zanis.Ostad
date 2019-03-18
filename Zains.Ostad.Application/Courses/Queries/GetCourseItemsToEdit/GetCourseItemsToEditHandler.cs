using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zains.Ostad.Application.AutoMapperProfiles;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses.Queries.GetCourseItemsToEdit
{
    public class GetCourseItemsToEditHandler : IRequestHandler<GetCourseItemsToEdit, PagenatedList<CourseItemViewModel>>
    {
        private readonly IRepository<CourseItem, long> _courseItemRepo;

        public GetCourseItemsToEditHandler(IRepository<CourseItem, long> courseItemRepo)
        {
            _courseItemRepo = courseItemRepo;
        }

        public async Task<PagenatedList<CourseItemViewModel>> Handle(GetCourseItemsToEdit request,
            CancellationToken cancellationToken)
        {
            var queryable = _courseItemRepo.GetQueriable().Where(x => x.ContentType == ContentType.Video);
            if (request.EditStatus.HasValue)
                queryable = queryable.Where(x => x.LatestEditStatus == request.EditStatus);
            
            return new PagenatedList<CourseItemViewModel>
            {
                AllCount = queryable.Count(),
                Items = queryable.Pagenate(request).Select(CourseProfile.CourseItemProjection).ToList()
            };
        }
    }
}