using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Teachers.Commands.ActiveDeactiveCourse
{
    public class ActiveDeactiveCourseCommandHandler : IRequestHandler<ActiveCourseCommand, Response>,
        IRequestHandler<DeactiveCourseCommand, Response>
    {
        public Task<Response> Handle(ActiveCourseCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> Handle(DeactiveCourseCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}