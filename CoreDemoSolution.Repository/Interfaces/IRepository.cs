namespace CoreDemoSolution.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class
    {
        IQueryable<T> AllIncluding(bool asNoTracking = false, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetAll(bool asNoTracking = false);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, bool asNoTracking = false);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false);
        int Count();
        List<T> Tolist(Expression<Func<T, bool>> predicate, bool asNoTracking = false);
        //T GetSingle(Guid id);
        T GetSingle(Expression<Func<T, bool>> predicate, bool asNoTracking = false);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T GetSingleWithoutTracking(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetSingleWithoutTrackingAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, bool asNoTracking = false);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        void Save();
        Task SaveAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
