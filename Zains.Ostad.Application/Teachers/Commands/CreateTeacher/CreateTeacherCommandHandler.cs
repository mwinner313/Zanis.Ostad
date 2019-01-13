using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zains.Ostad.Application.Users;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Response>
    {
        private readonly IAppUserManager _userManager;

        public CreateTeacherCommandHandler(IAppUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var res = await _userManager.CreateAsync(new User
            {
                TeacherCode = request.TeacherCode,
                UserName = request.TeacherCode,
            });
            return res.Succeeded ? Response.Success() : Response.Failed();
        }
    }
}