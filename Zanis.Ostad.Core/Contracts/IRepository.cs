using System.Linq;
using System.Threading.Tasks;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Contracts
{
    public interface IRepository<T,TKey> where T :class  , IBaseEntity<TKey>
    {
        Task AddAsync(T model);
        Task EditAsync(T model);
        Task Delete(TKey model);
        IQueryable<T> GetQueriable();
    }
}