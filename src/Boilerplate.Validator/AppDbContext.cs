namespace Boilerplate.Validator
{
    #region << Using >>

    using Microsoft.EntityFrameworkCore;

    #endregion

    public sealed class AppDbContext : DbContext
    {
        #region Properties

        public DbSet<Item> Items { get; set; }

        #endregion

        #region Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {
            Database.EnsureCreated();
        }

        #endregion
    }
}