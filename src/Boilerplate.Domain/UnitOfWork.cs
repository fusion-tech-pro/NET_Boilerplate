namespace Boilerplate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private bool _disposed;
        private Dictionary<Type, object> _repositories;
        
        public UnitOfWork(TContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public TContext DbContext { get; }
        
        public void ChangeDatabase(string database)
        {
            var connection = DbContext.Database.GetDbConnection();
            if (connection.State.HasFlag(ConnectionState.Open))
            {
                connection.ChangeDatabase(database);
            }
            else
            {
                var connectionString = Regex.Replace(connection.ConnectionString.Replace(" ", ""), @"(?<=[Dd]atabase=)\w+(?=;)", database, RegexOptions.Singleline);
                connection.ConnectionString = connectionString;
            }
            
            var items = DbContext.Model.GetEntityTypes();
            foreach (var item in items)
            {
                if (item is IConventionEntityType entityType)
                {
                    entityType.SetSchema(database);
                }
            }
        }
        
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(DbContext);
            }

            return (IRepository<TEntity>)_repositories[type];
        }
        
/*CODEREVIEW: to keep code consistent use classic function here and beneath*/
        public int ExecuteSqlCommand(string sql, params object[] parameters) => DbContext.Database.ExecuteSqlRaw(sql, parameters);
        
        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class => DbContext.Set<TEntity>().FromSqlRaw(sql, parameters);
        
        public int SaveChanges(bool ensureAutoHistory = false)
        {
            return DbContext.SaveChanges(ensureAutoHistory);
        }
        
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
        {
            return await DbContext.SaveChangesAsync(ensureAutoHistory);
        }
                
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // clear repositories
                    _repositories?.Clear();

                    // dispose the db context.
                    DbContext.Dispose();
                }
            }

            _disposed = true;
        }
    }
}