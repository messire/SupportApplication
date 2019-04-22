using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using NLog;
using SupportApplication.Core.Context;
using SupportApplication.Core.Exception;

namespace SupportApplication.Core.Repository
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        #region Variables

        private readonly DbContext _context;

        private readonly DbSet<TEntity> _dbSet;

        private readonly Logger _log;

        #endregion

        #region Constructor

        public Repository()
        {
            _context = new SupportEntities();
            _dbSet = _context.Set<TEntity>();
            _log = LogManager.GetCurrentClassLogger();
        }

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _log = LogManager.GetCurrentClassLogger();
        }

        #endregion

        #region Destructor

        ~Repository() => Dispose(false);

        #endregion

        #region CRUD

        public int Create(TEntity item)
        {
            _dbSet.Add(item);
            return SaveChanges();
        }

        public int Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return SaveChanges();
        }

        public int Remove(TEntity item)
        {
            _dbSet.Remove(item);
            return SaveChanges();
        }

        #endregion

        #region Selection

        public TEntity FindById(string guid) => _dbSet.Find(guid);

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

        private int SaveChanges()
        {
            SupportApplicationErrors error;
            string logMessage;

            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                error = SupportApplicationErrors.ConcurrencyException;
                logMessage = e.Message;
            }
            catch (DbUpdateException e)
            {
                error = SupportApplicationErrors.DatabaseUpdateException;
                logMessage = e.Message;
            }
            catch (CommitFailedException e)
            {
                error = SupportApplicationErrors.DatabaseCommitException;
                logMessage = e.Message;
            }
            catch (System.Exception e)
            {
                error = SupportApplicationErrors.UnexpectedException;
                logMessage = e.Message;
            }

            LogError($"Database error: {error}{Environment.NewLine}Message: {logMessage}");
            return (int)error;
        }

        private void LogError(string message) => _log.Error(message);

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
