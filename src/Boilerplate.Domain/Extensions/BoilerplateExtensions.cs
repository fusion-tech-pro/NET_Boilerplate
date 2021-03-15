namespace Boilerplate.Domain
{
    #region << Using >>

    using FluentValidation.AspNetCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    #endregion

    public static class BoilerplateExtensions
    {
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();

            return services;
        }

        public static IServiceCollection AddFluentValidation<TContext>(this IServiceCollection services)
        {
            services.AddMvc(opt => opt.Filters.Add(typeof(ValidateModelStateFilter)))
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TContext>());

            return services;
        }

        public static IServiceCollection AddBoilerplateDependencies<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
        {
            services.AddUnitOfWork<TContext>(connectionString);
            services.AddFluentValidation<TContext>();

            return services;
        }

        public static IServiceCollection AddAutoMapper<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddAutoMapper(typeof(TContext));

            return services;
        }
    }
}