using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SupportApplication.Core.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Create(TEntity item);

        int Remove(TEntity item);

        int Update(TEntity item);

        TEntity FindById(string guid);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);

        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

        IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
