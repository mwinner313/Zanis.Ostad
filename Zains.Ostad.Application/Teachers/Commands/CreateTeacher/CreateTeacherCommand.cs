using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommand:IRequest<Response>
    {
        public string TeacherCode { get; set; }
        public string FullName { get; set; }
    }
}