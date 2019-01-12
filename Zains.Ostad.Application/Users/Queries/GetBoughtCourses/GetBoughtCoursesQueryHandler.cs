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
    public class GetBoughtCoursesQueryHandler : IRequestHandler<GetBoughtCoursesQuery, List<UserCourseDto>>
    {
        private readonly IRepository<StudentCourseMapping, long> _studentCourseRepo;
        private readonly IWorkContext _workContext;

        public GetBoughtCoursesQueryHandler(IRepository<StudentCourseMapping, long> studentCourseRepo, IWorkContext workContext)
        {
            _studentCourseRepo = studentCourseRepo;
            _workContext = workContext;
        }

        public Task<List<UserCourseDto>> Handle(GetBoughtCoursesQuery request, CancellationToken cancellationToken)
        {
            return _studentCourseRepo.GetQueriable().Where(x => x.StudentId == _workContext.CurrentUserId)
                .OrderByDescending(x=>x.Id)
                .Pagenate(request).Select(x=>x.Course).Select(CourseProfile.ProjectionForUser).ToListAsync(cancellationToken);
        }
    }
}