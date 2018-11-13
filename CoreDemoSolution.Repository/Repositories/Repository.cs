namespace CoreDemoSolution.Repository.Repositories
{
    using CoreDemoSolution.Data;
    using CoreDemoSolution.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repository<T> : IRepository<T> where T : class
    {

        internal readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public virtual void BeginTransaction()
        {
            _context.Database.BeginTransactionAsync();
        }
        public virtual void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }
        public virtual void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public virtual IEnumerable<T> GetAll(bool asNoTracking = false)
        {
            if (asNoTracking)
                return _context.Set<T>().AsNoTracking();
            else
                return _context.Set<T>();
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, bool asNoTracking = false)
        {
            if (asNoTracking)
                return _context.Set<T>().AsNoTracking().Where(predicate);
            else
                return _context.Set<T>().Where(predicate);
        }

        public List<T> Tolist(Expression<Func<T, bool>> predicate, bool asNoTracking = false)
        {
            if (asNoTracking)
                return _context.Set<T>().AsNoTracking().Where(predicate).ToList();
            else
                return _context.Set<T>().Where(predicate).ToList();
        }

        public virtual async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false)
        {
            if (asNoTracking)
                return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
            else
                return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual int Count()
        {
            return _context.Set<T>().AsNoTracking().Count();
        }

        public virtual IQueryable<T> AllIncluding(bool asNoTracking = false, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            if (asNoTracking) query = query.AsNoTracking();

            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return query;
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, bool asNoTracking = false)
        {
            if (asNoTracking)
                return _context.Set<T>().AsNoTracking().FirstOrDefault(predicate);
            else
                return _context.Set<T>().FirstOrDefault(predicate);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false)
        {
            if (asNoTracking)
                return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
            else
                return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return query.Where(predicate).FirstOrDefault();
        }

        public T GetSingleWithoutTracking(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return query.Where(predicate).FirstOrDefault();
        }


        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> GetSingleWithoutTrackingAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, bool asNoTracking = false)
        {
            if (asNoTracking)
                return _context.Set<T>().AsNoTracking().Where(predicate);
            else
                return _context.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            _context.Set<T>().Add(entity);
        }

        public virtual void DeleteAll(List<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
        }

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> entities = _context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                _context.Entry<T>(entity).State = EntityState.Deleted;
            }
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
