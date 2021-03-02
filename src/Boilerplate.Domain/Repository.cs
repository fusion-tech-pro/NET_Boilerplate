namespace Boilerplate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _context;
        
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
   
        public virtual T First(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.First(predicate);
        }
        
        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }
        
        public T FirstOrDefault()
        {
            return _dbSet.FirstOrDefault();
        }
        
        public virtual IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }
        
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }

        public virtual T Find(params object[] keys)
        {
            return _dbSet.Find(keys);
        }
        
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        
        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }
        
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        
        public void DeleteRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }
        
        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        
        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }
        
        public virtual Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
        
        public Task<T> FirstOrDefaultAsync()
        {
            return _dbSet.FirstOrDefaultAsync();
        }
        
        public IOrderedQueryable<T> OrderBy<K>(Expression<Func<T, K>> predicate)
        {
            return _dbSet.OrderBy(predicate);
        }
        
        public IQueryable<IGrouping<K, T>> GroupBy<K>(Expression<Func<T, K>> predicate)
        {
            return _dbSet.GroupBy(predicate);
        }
    }
}