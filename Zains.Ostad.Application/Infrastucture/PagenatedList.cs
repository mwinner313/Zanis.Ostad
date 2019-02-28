using System.Collections.Generic;

namespace Zains.Ostad.Application.Infrastucture
{
    public class PagenatedList<T> where T :class
    {
        public List<T> Items { get; set; }
        public int AllCount { get; set; }
    }
}