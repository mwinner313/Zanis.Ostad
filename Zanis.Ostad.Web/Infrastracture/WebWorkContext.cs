using Microsoft.AspNetCore.Http;
using Zanis.Ostad.Core.Contracts;

namespace Zanis.Ostad.Web.Infrastracture
{
    public class WebWorkContext:IWorkContext
    {
        private readonly IHttpContextAccessor _accessor;

        public WebWorkContext(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public long CurrentUserId => _accessor.HttpContext.User.GetId();
    }
}
