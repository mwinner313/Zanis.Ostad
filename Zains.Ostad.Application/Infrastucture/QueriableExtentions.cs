using System.Linq;
using Zains.Ostad.Application.Users.Queries.GetBoughtCourses;
using Zanis.Ostad.Core.Infrastucture;

namespace Zains.Ostad.Application.Infrastucture
{
    public static class QueriableExtentions
    {
        public static IQueryable<T> Pagenate<T>(this IQueryable<T> queryable,Pagenation pagenation)
        {
            return queryable.Skip(pagenation.PageOffset).Take(pagenation.PageSize);
        }
    }
}