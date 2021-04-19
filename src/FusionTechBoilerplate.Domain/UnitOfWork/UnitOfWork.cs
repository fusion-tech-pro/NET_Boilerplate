namespace FusionTechBoilerplate.Domain
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Transactions;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    #endregion

    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        #region Properties

        public TContext DbContext { get; }

        private bool _disposed;

        private Dictionary<Type, object> _repositories;

        #endregion

        #region Constructors

        public UnitOfWork(TContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region Interface Implementations

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
                if (item is IConventionEntityType entityType)
                    entityType.SetSchema(database);
        }

        public void BeginTransactionAsync( Func<Task> transactionAction)
        {
            transactionAction.Invoke();
        }

        public Task<T> BeginTransactionAsync<T>(Func<T> transactionAction)
        {
            throw new NotImplementedException();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (this._repositories == null)
                this._repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!this._repositories.ContainsKey(type))
                this._repositories[type] = new Repository<TEntity>(DbContext);

            return (IRepository<TEntity>)this._repositories[type];
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlRaw(sql, parameters);
        }

        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return DbContext.Set<TEntity>().FromSqlRaw(sql, parameters);
        }

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

        /*public void BeginTransaction()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                using (var connection = new SqlConnection("..."))
                {
                    connection.Open();

                    /*var sqlCommand = new SqlCommand();
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText =
                            @"UPDATE Blogs SET Rating = 5" +
                            " WHERE Name LIKE '%Entity Framework%'";

                    sqlCommand.ExecuteNonQuery();

                    using (var context =
                            new BloggingContext(connection, contextOwnsConnection: false))
                    {
                        var query = context.Posts.Where(p => p.Blog.Rating > 5);
                        foreach (var post in query)
                            post.Title += "[Cool Blog]";

                        context.SaveChanges();
                    }#1#
                }

                scope.Complete();
            }
        }*/

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
                if (disposing)
                {
                    // clear repositories
                    this._repositories?.Clear();

                    // dispose the db context.
                    DbContext.Dispose();
                }

            this._disposed = true;
        }
    }
}