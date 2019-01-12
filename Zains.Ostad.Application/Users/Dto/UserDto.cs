using System.Collections.Generic;
using Zains.Ostad.Application.Lessons.Queries.GetLesson;

namespace Zains.Ostad.Application.Users.Dto
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string AvatarPath { get; set; }
        public long Id { get; set; }
        public List<string> Roles { get; set; }
    }
}