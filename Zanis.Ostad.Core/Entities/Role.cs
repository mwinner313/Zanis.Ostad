using Microsoft.AspNetCore.Identity;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities
{
    public class Role:IdentityRole<long>,IBaseEntity<long>
    {
        
    }
}