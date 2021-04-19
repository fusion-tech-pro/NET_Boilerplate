namespace FusionTechBoilerplate.Domain
{
    #region << Using >>

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        #region Properties

        TContext DbContext { get; }

        #endregion
    }

    public interface IUnitOfWork : IDisposable
    {
        void ChangeDatabase(string database);

        void BeginTransactionAsync (Func<Task> transactionAction);

        IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        int SaveChanges(bool ensureAutoHistory = false);

        Task<int> SaveChangesAsync(bool ensureAutoHistory = false);

        int ExecuteSqlCommand(string sql, params object[] parameters);

        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;
    }
}