using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses.Queries.HasAccessToFile
{
    public class HasAccessToFileQueryHandler:IRequestHandler<HasAccessToFileQuery,Response>
    {
        private readonly IRepository<CourseItem, long> _courseItemRepository;
        private readonly IRepository<StudentCourseMapping, long> _studentCourseRepository;
        public HasAccessToFileQueryHandler(IRepository<CourseItem,long> courseItemRepository, IRepository<StudentCourseMapping, long> studentCourseRepository)
        {
            _courseItemRepository = courseItemRepository;
            _studentCourseRepository = studentCourseRepository;
        }

        public async Task<Response> Handle(HasAccessToFileQuery request, CancellationToken cancellationToken)
        {
            if (!CourseItemFileExists(request))
                return Response.UnKnown();
            if (HasAccessToCourseAndCourseItem(request))
                return Response.Success();
            if(IsPreview(request.FilePath))
                return Response.Success();
            return Response.Failed();
        }

        private bool IsPreview(string requestFilePath)
        {
          return  _courseItemRepository.GetQueryable().First(x => requestFilePath.Contains(x.FilePath)).IsPreview;
        }

        private bool HasAccessToCourseAndCourseItem(HasAccessToFileQuery request)
        {
            return _studentCourseRepository.GetQueryable().Any(x =>
                x.StudentId == request.UserId && x.Course.Contents.Any(c => request.FilePath.Contains(c.FilePath)));
        }

        private bool CourseItemFileExists(HasAccessToFileQuery request)
        {
            return _courseItemRepository.GetQueryable().Any(x => request.FilePath.Contains(x.FilePath));
        }
    }
}