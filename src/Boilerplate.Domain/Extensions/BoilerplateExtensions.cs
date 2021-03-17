namespace Boilerplate.Domain
{
    #region << Using >>

    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

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
            services.AddAuthorizationJWT();

            return services;
        }

        public static IServiceCollection AddBoilerplateDependencies<TDbContext, TServicesContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
        {
            services.AddUnitOfWork<TDbContext>(connectionString);
            services.AddFluentValidation<TDbContext>();
            services.AddScrutor<TServicesContext>();
            services.AddAutoMapper(typeof(TDbContext));
            services.AddAuthorizationJWT();

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

        public static IServiceCollection AddAuthorizationJWT(this IServiceCollection services)
        {
            services.AddAuthorization(auth =>
                                      {
                                          auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                                                                   .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                                                   .RequireAuthenticatedUser()
                                                                   .Build());
                                      });

            services.AddAuthentication(x => x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                                  {
                                      opt.RequireHttpsMetadata = false;
                                      opt.TokenValidationParameters = new TokenValidationParameters
                                                                      {
                                                                              ValidateIssuer = true,
                                                                              ValidIssuer = Token.ISSUER,

                                                                              ValidateAudience = true,
                                                                              ValidAudience = Token.AUDIENCE,

                                                                              ValidateLifetime = true,

                                                                              IssuerSigningKey = Token.GetSymmetricSecurityKey(),
                                                                              ValidateIssuerSigningKey = true
                                                                      };
                                  });

            return services;
        }
    }
}