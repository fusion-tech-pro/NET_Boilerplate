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
        
        T First(ISpecification<T> predicate);
        
        T FirstOrDefault(ISpecification<T> predicate);
        
        T FirstOrDefault();
        
        Task<T> FirstOrDefaultAsync();
        
        IQueryable<T> GetAll();
        
        IQueryable<T> FindBy(ISpecification<T> predicate);
        
        bool Any(ISpecification<T> predicate);
        
        T Find(params object[] keys);
        
        void Add(T entity);
        
        void AddRange(IEnumerable<T> entities);
        
        void Delete(T entity);
        
        void DeleteRange(IEnumerable<T> entity);
        
        void Update(T entity);
    }
}