namespace FusionTechBoilerplate.API
{
    #region << Using >>

    using FusionTechBoilerplate.Domain;
    using FusionTechBoilerplate.Authentication;
    using FusionTechBoilerplate.Models;
    using FusionTechBoilerplate.Services;
    using FusionTechBoilerplate.Utilities;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using FusionTechBoilerplate.Utilities.EmailSender;

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
            var smtp = Configuration.GetSection(EmailOptions.SettingsSectionKey).Get<EmailOptions>();

            services.AddBoilerplateDependencies<AppDbContext, IItemService>(Configuration.GetConnectionString("DefaultConnection"));
            services.AddEmailSender(smtp);
            services.AddAuthorizationJWT<AppDbContext, IdentityUser>();
            services.AddControllersWithViews();
            services.AddCors();
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
        {
            dbContext.Database.Migrate();

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

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapControllerRoute(
                                                              "default",
                                                              "{controller=Item}/{action=Get}/{id?}");
                             });
        }
    }
}