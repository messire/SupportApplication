using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SupportApplication.Core.Repository
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        #region Variables

        readonly DbContext _context;

        readonly DbSet<TEntity> _dbSet;

        #endregion

        #region Constructor

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        #endregion

        #region Destructor

        ~Repository()
        {
            Dispose(false);
        }

        #endregion

        #region CRUD

        public void Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        #endregion

        #region Selection

        public TEntity FindById(Guid guid) => _dbSet.Find(guid);

        public IEnumerable<TEntity> GetAll() => _dbSet.AsNoTracking().ToList();

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate) =>
            _dbSet.AsNoTracking().Where(predicate).ToList();

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties) =>
            Include(includeProperties).ToList();

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties) =>
            Include(includeProperties).AsEnumerable().Where(predicate).ToList();

        #endregion

        #region Helpers

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        #endregion

        #region Dispose

        private void Dispose(bool disposing)
        {
            if (disposing) _context?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
