using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.AutoMapperProfiles;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Lessons.Queries.GetLesson;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Users.Queries.GetBoughtCourses
{
    public class GetBoughtCoursesQueryHandler : IRequestHandler<GetBoughtCoursesQuery, PagenatedList<UserCourseDto>>
    {
        private readonly IRepository<StudentCourseMapping, long> _studentCourseRepo;
        private readonly IWorkContext _workContext;

        public GetBoughtCoursesQueryHandler(IRepository<StudentCourseMapping, long> studentCourseRepo, IWorkContext workContext)
        {
            _studentCourseRepo = studentCourseRepo;
            _workContext = workContext;
        }

        public async Task<PagenatedList<UserCourseDto>> Handle(GetBoughtCoursesQuery request, CancellationToken cancellationToken)
        {
            var querable = _studentCourseRepo.GetQueryable().Where(x => x.StudentId == _workContext.CurrentUserId);
            return new PagenatedList<UserCourseDto>
            {
                Items = await querable
                    .OrderByDescending(x=>x.Id)
                    .Pagenate(request).Select(x=>x.Course).Select(CourseProfile.ProjectionForUser).ToListAsync(cancellationToken),
                AllCount =  querable.Count()
            };
        }
    }
}