using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Zanis.Ostad.Core.Dtos;

namespace Zanis.Ostad.Web.Infrastracture
{
    public class SecureZanisProtectedFilesMiddleware
    {
        private readonly RequestDelegate _next;

        public SecureZanisProtectedFilesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IMediator _mediator)
        {
            if (httpContext.Request.Path.Value.ToLower().Contains("files"))
            {
                if (httpContext.User == null ||!httpContext.User.Identity.IsAuthenticated)
                {
                    httpContext.Response.Redirect("/access-unauthorized");
                    return;
                }

                if (await HasNotAccessToThisFile(httpContext, _mediator))
                {
                    httpContext.Response.Redirect("/access-unauthorized");
                    return;
                }

                await _next(httpContext);
            }
            else
            {
                await _next(httpContext);
            }
        }

        private  async Task<bool> HasNotAccessToThisFile(HttpContext httpContext,IMediator mediator)
        {
            var examSamplesRes =
                await mediator.Send(
                    new Zains.Ostad.Application.ExamSamples.Queries.HasAccessToFile.HasAccessToFileQuery(
                        httpContext.User.GetId(),httpContext.Request.Path.Value));
            var coursesRes =
                await mediator.Send(
                    new Zains.Ostad.Application.Courses.Queries.HasAccessToFile.HasAccessToFileQuery(
                        httpContext.User.GetId(),httpContext.Request.Path.Value));
            return examSamplesRes.Status == ResponseStatus.Fail || coursesRes.Status == ResponseStatus.Fail;
        }
    }
}
