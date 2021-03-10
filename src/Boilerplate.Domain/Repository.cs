namespace Boilerplate.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public class Repository<T> : IRepository<T> where T : class
    {
        #region Properties

        private readonly DbContext _context;

        private readonly DbSet<T> _dbSet;

        #endregion

        #region Constructors

        public Repository(DbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }

        #endregion

        #region Interface Implementations

        public virtual T First(ISpecification<T> predicate)
        {
            return this._dbSet.First(predicate.IsSatisfiedBy);
        }

        public virtual T FirstOrDefault(ISpecification<T> predicate)
        {
            return this._dbSet.FirstOrDefault(predicate.IsSatisfiedBy);
        }

        public T FirstOrDefault()
        {
            return this._dbSet.FirstOrDefault();
        }

        public virtual IQueryable<T> GetAll()
        {
            return this._dbSet.AsNoTracking();
        }

        public virtual IQueryable<T> FindBy(ISpecification<T> spec)
        {
            return this._dbSet.Where(r => spec.IsSatisfiedBy(r));
        }

        public bool Any(ISpecification<T> predicate)
        {
            return this._dbSet.Any(predicate.IsSatisfiedBy);
        }

        public virtual T Find(object key, params object[] keys)
        {
            return this._dbSet.Find(keys);
        }

        public virtual T Get(int id)
        {
            return this._dbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            this._dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            this._dbSet.AddRange(entities);
        }

        public virtual void Delete(T entity)
        {
            this._dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            this._dbSet.RemoveRange(entity);
        }

        public virtual void Update(T entity)
        {
            //_context.Entry(entity).State = EntityState.Modified;
            this._dbSet.Update(entity);
        }

        public virtual void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public virtual Task SaveChangesAsync()
        {
            return this._context.SaveChangesAsync();
        }

        public Task<T> FirstOrDefaultAsync(object unknown)
        {
            return this._dbSet.FirstOrDefaultAsync();
        }

        #endregion
    }
}