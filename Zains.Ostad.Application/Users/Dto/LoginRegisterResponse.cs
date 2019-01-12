
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Users.Dto
{
    public class LoginRegisterResponse:Response
    {
        public string BearerToken { get; set; }
        public UserDto User { get; set; }
    }
}