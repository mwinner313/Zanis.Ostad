using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Courses.Queries.GetCourseDetails;
using Zanis.Ostad.Web.Infrastracture;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class CoursesController:Controller
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> Get(long id)
        {
            return Ok(await _mediator.Send(new GetCourseDetailsQuery
            {
                CourseId = id,
                CurrentUserId =  User.GetId()
            }));
        }
    }
}
