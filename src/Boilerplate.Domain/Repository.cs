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

        public virtual T First(ISpecification<T> predicate)
        {
            return _dbSet.First(predicate.IsSatisfiedBy);
        }
        
        public virtual T FirstOrDefault(ISpecification<T> predicate)
        {
            return _dbSet.FirstOrDefault(predicate.IsSatisfiedBy);
        }
        
        public T FirstOrDefault()
        {
            return _dbSet.FirstOrDefault();
        }
        
        public virtual IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }
        
        public virtual IQueryable<T> FindBy(ISpecification<T> spec)
        {
            return _dbSet.Where(r => spec.IsSatisfiedBy(r));
        }

        public bool Any(ISpecification<T> predicate)
        {
            return _dbSet.Any(predicate.IsSatisfiedBy);
        }

        public virtual T Find(object key, params object[] keys)
        {
            return _dbSet.Find(keys);
        }

        public virtual T Get(int id)
        {
            return _dbSet.Find(id);
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
            //_context.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
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
    }
}