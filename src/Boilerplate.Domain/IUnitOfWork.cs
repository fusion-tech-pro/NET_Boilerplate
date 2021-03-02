namespace Boilerplate.Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext DbContext { get; }
    }
    public interface IUnitOfWork : IDisposable
    {
        void ChangeDatabase(string database);
        
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        
        int SaveChanges(bool ensureAutoHistory = false);
        
        Task<int> SaveChangesAsync(bool ensureAutoHistory = false);
        
        int ExecuteSqlCommand(string sql, params object[] parameters);
        
        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;
    }
}