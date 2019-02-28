using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Zains.Ostad.Application.Teachers.Commands.CreateTeacher;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Authorize(Roles = "Operator")]
    [Route("api/[controller]")]
    public class TeacherController : Controller
    {
        private readonly IMediator _mediator;
        public TeacherController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
