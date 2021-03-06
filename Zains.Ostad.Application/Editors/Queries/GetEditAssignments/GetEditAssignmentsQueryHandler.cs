using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.AutoMapperProfiles;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Editors.Queries.GetEditAssignments
{
    public class
        GetEditAssignmentsQueryHandler : IRequestHandler<GetEditAssignmentsQuery, PagenatedList<EditAssignmentViewModel>
        >
    {
        private readonly IRepository<EditAssignment, long> _repository;
        private readonly IWorkContext _workContext;

        public GetEditAssignmentsQueryHandler(IRepository<EditAssignment, long> repository, IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }

        public async Task<PagenatedList<EditAssignmentViewModel>> Handle(GetEditAssignmentsQuery request,
            CancellationToken cancellationToken)
        {
            var queryable = _repository.GetQueryable().Where(x => x.EditorId == _workContext.CurrentUserId)
                .OrderByDescending(x => x.CreatedOn).AsQueryable();
            
            if (request.Status.HasValue)
                queryable = queryable.Where(x => x.Status == request.Status);
            
            if (!string.IsNullOrEmpty(request.Search))
                queryable = queryable.Where(x => x.CourseItem.Title.Contains(request.Search) || x.Editor.FullName.Contains(request.Search));
            
            return new PagenatedList<EditAssignmentViewModel>
            {
                Items = queryable.Include(x => x.CourseItem)
                    .Include(x => x.CourseItem.Course)
                    .Include(x => x.CourseItem.Course.CourseCategory)
                    .Include(x => x.CourseItem.Course.Lessons)
                    .Include(x => x.CourseItem.Course.Teacher)
                    .Include(x => x.CourseItem.Course.Lessons.Select(l=>l.Lesson))
                    .Include(x => x.CourseItem.Course.Lessons.Select(l=>l.Lesson.Lesson))
                    .Include(x => x.CourseItem.Course.Lessons.Select(l=>l.Lesson.Grade))
                    .Include(x => x.CourseItem.Course.Lessons.Select(l=>l.Lesson.Field))
                    .Select(EditAssignmentProfile.Projection).AsQueryable().Pagenate(request).ToList(),
                AllCount = queryable.Count()
            };
        }
    }
}