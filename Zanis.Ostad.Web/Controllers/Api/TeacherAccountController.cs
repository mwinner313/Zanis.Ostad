using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Teachers.Commands.ActiveDeactiveCourse;
using Zains.Ostad.Application.Teachers.Commands.AddEditCourse;
using Zains.Ostad.Application.Teachers.Queries.GetProducedCourses;
using Zanis.Ostad.Web.Infrastracture;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Authorize(Roles="Teacher")]
    [Route("api/[controller]")]
    public class TeacherAccountController:BaseController
    {
        private readonly IMediator _mediator;

        public TeacherAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("courses")]
        public async Task<ActionResult<PagenatedList<CourseDto>>> GetProducedCourses(GetProducedCoursesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("courses/{id}")]
        public async Task<ActionResult<PagenatedList<CourseDto>>> GetCourseDetails(long id)
        {
            return Ok(await _mediator.Send(new GetProducedCourseDetailQuery
            {
                CourseId = id
            }));
        }

        [HttpPost("courses")]
        public async Task<ActionResult<PagenatedList<CourseDto>>> AddCourse(AddCourseCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [HttpPut("courses")]
        public async Task<ActionResult<PagenatedList<CourseDto>>> AddCourse(EditCourseCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
        [HttpPut("courses/deactive")]
        public async Task<ActionResult<PagenatedList<CourseDto>>> DeactiveCourse(DeactiveCourseCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
    }
}
