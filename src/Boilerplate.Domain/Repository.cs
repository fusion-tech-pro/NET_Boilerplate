namespace Boilerplate.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
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

        public virtual IQueryable<T> Get(Specification<T> spec = null)
        {
            var expression = spec?.ToExpression();

            return expression == null ? this._dbSet.AsQueryable() : this._dbSet.Where(expression);
        }

        public virtual void Add(T entity)
        {
            this._dbSet.Add(entity);
        }

        public virtual void Add(IEnumerable<T> entities)
        {
            this._dbSet.AddRange(entities);
        }

        public virtual void Delete(T entity)
        {
            this._dbSet.Remove(entity);
        }

        public void Delete(IEnumerable<T> entity)
        {
            this._dbSet.RemoveRange(entity);
        }

        public virtual void Update(T entity)
        {
            this._context.Entry(entity).State = EntityState.Modified;
            this._dbSet.Update(entity);
        }

        #endregion
    }
}