namespace Boilerplate.Domain
{
    #region << Using >>

    using System;
    using FluentValidation.AspNetCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Quartz;

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
            services.AddScrutor<TContext>();
            services.AddAutoMapper(typeof(TContext));
            services.AddQuartz();

            return services;
        }

        public static IServiceCollection AddBoilerplateDependencies<TDbContext, TServicesContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
        {
            services.AddUnitOfWork<TDbContext>(connectionString);
            services.AddFluentValidation<TDbContext>();
            services.AddScrutor<TServicesContext>();
            services.AddAutoMapper(typeof(TDbContext));
            services.AddBoilerplateQuartz();

            return services;
        }

        public static IServiceCollection AddScrutor<TContext>(this IServiceCollection services)
        {
            services.Scan(i =>
                                  i.FromAssemblyOf<TContext>()
                                   .InjectableAttributes()
                         );

            return services;
        }

        public static IServiceCollection AddBoilerplateQuartz(this IServiceCollection services, Action<IServiceCollectionQuartzConfigurator> quartzConfig = null)
        {
            services.AddQuartz(quartzConfig ?? (q => q.UseMicrosoftDependencyInjectionScopedJobFactory()));
            services.AddQuartzServer(opt => opt.WaitForJobsToComplete = true);

            return services;
        }
    }
}