namespace Boilerplate.Domain
{
    #region << Using >>

    using AutoMapper;
    using Boilerplate.Models;
    using FluentValidation.AspNetCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    #endregion

    public static class BoilerplateExtensions
    {
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();

            return services;
        }

        public static IServiceCollection AddFluentValidation<TContext>(this IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TContext>());

            return services;
        }

        public static IServiceCollection AddBoilerplateDependencies<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            services.AddUnitOfWork<TContext>();
            services.AddFluentValidation<TContext>();

            return services;
        }
    }
}