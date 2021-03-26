namespace Boilerplate.Domain
{
    #region << Using >>

    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Quartz;
    using Quartz.Impl;
    using Quartz.Spi;

    #endregion

    #region << Using >>

    #endregion

    public static class QuartzExtensions
    {
        public static IServiceCollection AddBoilerplateQuartz(this IServiceCollection services, Action<IServiceCollectionQuartzConfigurator> quartzConfig = null)
        {
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<TestJob>();
            services.AddSingleton(new JobSchedule(
                                                  typeof(TestJob),
                                                  "0/5 * * * * ?"));

            services.AddHostedService<QuartzService>();

            return services;
        }
    }
}