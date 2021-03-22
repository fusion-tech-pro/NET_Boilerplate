namespace Boilerplate.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get(Specification<T> predicate);

        void Add(T entity);

        void Add(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(IEnumerable<T> entity);

        void Update(T entity);
    }
}