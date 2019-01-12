using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Lessons.Dtos;
using Zains.Ostad.Application.Lessons.Queries;
using Zains.Ostad.Application.Lessons.Queries.GetLesson;
using Zains.Ostad.Application.Lessons.Queries.GetLessonList;
using Zanis.Ostad.Web.Infrastracture;

namespace Zanis.Ostad.Web.Controllers.Api
{

    [Route("api/[controller]")]
    public class GradeFieldLessonsController:BaseController
    {
        private readonly IMediator _mediator;

        public GradeFieldLessonsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetLessonQuery()
            {
                LessonId = id,
                RequestingUserId = User.Identity.IsAuthenticated? User.GetId():0
            }));
        }
        [HttpGet]
        public async Task<ActionResult<List<LessonFieldListViewModel>>> Get(GetLessonListQuery query)
        {
                return Ok(await _mediator.Send(query));
        }
    }
}
