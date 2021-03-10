namespace Boilerplate.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public interface IRepository<T> where T : class
    {
        void SaveChanges();

        Task SaveChangesAsync();

        T Get(int id);

        T First(ISpecification<T> predicate);

        T FirstOrDefault(ISpecification<T> predicate);

        T FirstOrDefault();

        Task<T> FirstOrDefaultAsync(object unknown);

        IQueryable<T> GetAll();

        IQueryable<T> FindBy(ISpecification<T> predicate);

        bool Any(ISpecification<T> predicate);

        T Find(object key, params object[] keys);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entity);

        void Update(T entity);
    }
}