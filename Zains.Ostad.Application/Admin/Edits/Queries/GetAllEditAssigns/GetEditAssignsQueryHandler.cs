using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.AutoMapperProfiles;
using Zains.Ostad.Application.Editors.Queries.GetEditAssignments;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Edits;

namespace Zains.Ostad.Application.Admin.Edits.Queries.GetAllEditAssigns
{
    public class GetEditAssignsQueryHandler:IRequestHandler<GetEditAssignsQuery,PagenatedList<EditAssignmentViewModel>>
    {
        private readonly IRepository<EditAssignment,long> _editAssignRepo;

        public GetEditAssignsQueryHandler(IRepository<EditAssignment, long> editAssignRepo)
        {
            _editAssignRepo = editAssignRepo;
        }
    
        public async Task<PagenatedList<EditAssignmentViewModel>> Handle(GetEditAssignsQuery request, CancellationToken cancellationToken)
        {
            var queryable = _editAssignRepo.GetQueryable()
                .OrderByDescending(x => x.CreatedOn).AsQueryable();
            
            if (request.Status.HasValue)
                queryable = queryable.Where(x => x.Status == request.Status);
            
            if (!string.IsNullOrEmpty(request.Search))
                queryable = queryable.Where(x => x.CourseItem.Title.Contains(request.Search) || x.Editor.FullName.Contains(request.Search));

            return new PagenatedList<EditAssignmentViewModel>
            {
                Items = queryable.Include(x => x.CourseItem)
                    .Include(x=>x.Editor)
                    .Include(x => x.CourseItem.Course)
                    .Include(x => x.CourseItem.Course.CourseCategory)
                    .Include(x => x.CourseItem.Course.Teacher)
                    .Include(x => x.CourseItem.Course.Lessons)
                    .Include(x => x.CourseItem.Course.Lessons.Select(l=>l.Lesson))
                    .Include(x => x.CourseItem.Course.Lessons.Select(l=>l.Lesson.Lesson))
                    .Include(x => x.CourseItem.Course.Lessons.Select(l=>l.Lesson.Field))
                    .Include(x => x.CourseItem.Course.Lessons.Select(l=>l.Lesson.Grade))
                    .Select(EditAssignmentProfile.Projection).AsQueryable().Pagenate(request).ToList(),
                AllCount = queryable.Count()
            };
        }
    }
}