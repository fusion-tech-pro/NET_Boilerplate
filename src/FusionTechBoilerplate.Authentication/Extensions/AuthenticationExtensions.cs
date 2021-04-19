namespace FusionTechBoilerplate.Authentication
{
    #region << Using >>

    using System;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    #endregion

    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthorizationJWT<TDbContext, TIdentityUser>(this IServiceCollection services, Action<IdentityOptions> identityOptions = null)
                where TIdentityUser : IdentityUser, new()
                where TDbContext : IdentityDbContext<TIdentityUser>
        {
            services.AddScoped<IAuthService, AuthService<TIdentityUser>>();

            services.AddDefaultIdentity<TIdentityUser>(identityOptions ?? (r => { }))
                    .AddEntityFrameworkStores<TDbContext>();

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