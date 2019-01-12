using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Repository
{
    public class EfRepository<T, TKey> : IRepository<T, TKey> where T : class, IBaseEntity<TKey>
    {
        private readonly ApplicationDbContext _db;
        public EfRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(T model)
        {
            _db.Set<T>().Add(model);
            await _db.SaveChangesAsync();
        }

        public async Task EditAsync(T model)
        {
            var editing = _db.Set<T>().First(x => x.Id.ToString() == model.Id.ToString());
            _db.Entry(editing).CurrentValues.SetValues(model);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(TKey model)
        {
            var deleting = _db.Set<T>().First(x => x.Id.ToString() == model.ToString());
            _db.Entry(deleting).State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }

        public IQueryable<T> GetQueriable()
        {
            return _db.Set<T>();
        }
    }
}