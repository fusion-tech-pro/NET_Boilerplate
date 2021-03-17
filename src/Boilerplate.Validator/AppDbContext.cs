namespace Boilerplate.Models
{
    #region << Using >>

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public sealed class AppDbContext : IdentityDbContext<IdentityUser>
    {
        #region Properties

        public DbSet<Item> Items { get; set; }

        #endregion

        #region Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options) { }

        #endregion
    }
}