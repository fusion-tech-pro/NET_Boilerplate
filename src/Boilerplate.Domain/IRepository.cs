namespace Boilerplate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IRepository<T> where T : class
    {
        void SaveChanges();
        
        Task SaveChangesAsync();
        
        T First(Expression<Func<T, bool>> predicate);
        
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        
        T FirstOrDefault();
        
        Task<T> FirstOrDefaultAsync();
        
        IQueryable<T> GetAll();
        
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        
        bool Any(Expression<Func<T, bool>> predicate);
        
        T Find(params object[] keys);
        
        void Add(T entity);
        
        void AddRange(IEnumerable<T> entities);
        
        void Delete(T entity);
        
        void DeleteRange(IEnumerable<T> entity);
        
        void Update(T entity);
        
        IOrderedQueryable<T> OrderBy<K>(Expression<Func<T, K>> predicate);
        
        IQueryable<IGrouping<K, T>> GroupBy<K>(Expression<Func<T, K>> predicate);
    }
}