namespace FusionTechBoilerplate.Utilities
{
    #region << Using >>

    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Quartz;

    #endregion

    public static class QuartzExtensions
    {
        public static IServiceCollection AddBoilerplateQuartz<TContext>(this IServiceCollection services, Action<IServiceCollectionQuartzConfigurator> quartzConfig = null)
        {
            services.Scan(r => r.FromAssemblyOf<TContext>()
                                .AddClasses(c => c.AssignableTo<IJob>())
                                .AsSelf()
                                .WithLifetime(ServiceLifetime.Transient));

            services.AddSingleton<QuartzService>();

            return services;
        }
    }
}
