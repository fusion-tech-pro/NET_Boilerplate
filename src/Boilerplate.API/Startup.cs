namespace Boilerplate.API
{
    #region << Using >>

    using Boilerplate.Domain;
    using Boilerplate.Models;
    using Boilerplate.Services;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    #endregion

    public class Startup
    {
        #region Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region Constructors

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBoilerplateDependencies<AppDbContext, IItemService>(Configuration.GetConnectionString("DefaultConnection"));
            services.AddAuthorizationJWT<AppDbContext, IdentityUser>();
            services.AddControllersWithViews();
            services.AddCors();
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Item/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(builder => builder
                                   .AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapControllerRoute(
                                                              "default",
                                                              "{controller=Item}/{action=Get}/{id?}");
                             });
        }
    }
}