using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Teachers.Commands.AddEditCourse
{
    public class AddEditCourseCommandHandler:IRequestHandler<EditCourseCommand,Response>,IRequestHandler<AddCourseCommand,Response>
    {
        private readonly IRepository<Course, long> _courseRepository;
        public AddEditCourseCommandHandler(IRepository<Course, long> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Task<Response> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
        
        public Task<Response> Handle(EditCourseCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}