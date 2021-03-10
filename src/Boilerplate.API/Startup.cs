namespace Boilerplate.API
{
    #region << Using >>

    using Boilerplate.Domain;
    using Boilerplate.Models;
    using Boilerplate.Services;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBoilerplateDependencies<AppDbContext>(Configuration.GetConnectionString("DefaultConnection"));
            services.AddScoped<IItemService, ItemService>();
            services.AddControllersWithViews();
            services.AddCors();
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
                                                              "{controller=Home}/{action=Index}/{id?}");
                             });
        }
    }
}