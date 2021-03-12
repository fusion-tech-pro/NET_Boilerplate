namespace Boilerplate.API
{
    #region << Using >>

    using Boilerplate.Domain;
    using Boilerplate.Models;
    using Boilerplate.Services;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
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
            services.AddBoilerplateDependencies<AppDbContext>(Configuration.GetConnectionString("DefaultConnection"));
            services.AddScoped<IItemService, ItemService>();
            services.AddControllersWithViews();
            services.AddCors();
            services.AddAutoMapper(typeof(Startup));
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