namespace FusionTechBoilerplate.Domain
{
    #region << Using >>

    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    #endregion

    public static class BoilerplateExtensions
    {
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services, string connectionString, SQLServerType sqlType)
                where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>  {
            switch (sqlType)
            {
                case SQLServerType.MSSQL:
                    options.UseSqlServer(connectionString);
                    break;
                case SQLServerType.PostgreSQl:
                    options.UseNpgsql(connectionString);
                    break;
                default:
                    throw new NotImplementedException();
            };   
            });
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

        public static IServiceCollection AddBoilerplateDependencies<TContext>(this IServiceCollection services, string connectionString, SQLServerType sqlType)
                where TContext : DbContext
        {
            services.AddUnitOfWork<TContext>(connectionString, sqlType);
            services.AddFluentValidation<TContext>();
            services.AddExceptionFilter();
            services.AddScrutor<TContext>();
            services.AddAutoMapper(typeof(TContext));

            return services;
        }

        public static IServiceCollection AddBoilerplateDependencies<TDbContext, TServicesContext>(this IServiceCollection services, string connectionString, SQLServerType sqlType)
                where TDbContext : DbContext
        {
            services.AddUnitOfWork<TDbContext>(connectionString, sqlType);
            services.AddFluentValidation<TDbContext>();
            services.AddExceptionFilter();
            services.AddScrutor<TServicesContext>();
            services.AddAutoMapper(typeof(TDbContext));

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

        public static IServiceCollection AddExceptionFilter(this IServiceCollection service)
        {
            service.AddMvc(opt => opt.Filters.Add(typeof(ExceptionFilter))).SetCompatibilityVersion(CompatibilityVersion.Latest);

            return service;
        }
    }
}