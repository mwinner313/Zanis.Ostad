using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Principal;

namespace Zanis.Ostad.Web.Infrastracture
{
    public static class IdentityExtentions
    {
        public static long GetId(this IPrincipal principal)
        {
            var claimsPrincipal = principal as ClaimsPrincipal;
            return claimsPrincipal is null || !claimsPrincipal.Identity.IsAuthenticated ? default : long.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
