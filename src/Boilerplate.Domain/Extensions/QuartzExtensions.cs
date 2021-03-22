namespace Boilerplate.Domain
{
    #region << Using >>

    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Quartz;

    #endregion

    public static class QuartzExtensions
    {
        public static IServiceCollection AddBoilerplateQuartz(this IServiceCollection services, Action<IServiceCollectionQuartzConfigurator> quartzConfig = null)
        {
            services.AddQuartz(q =>
                               {
                                   q.UseMicrosoftDependencyInjectionScopedJobFactory();

                                   var jobKey = new JobKey("HelloWorldJob");
                                   q.AddJob<TestJob>(opts => opts.WithIdentity(jobKey));

                                   q.AddTrigger(opts => opts
                                                        .ForJob(jobKey)
                                                        .WithIdentity("HelloWorldJob-trigger")
                                                        .WithCronSchedule("0/5 * * * * ?"));
                               });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }
    }

    public class TestJob : IJob
    {
        #region Properties

        private readonly ILogger<TestJob> _logger;

        #endregion

        #region Constructors

        public TestJob(ILogger<TestJob> logger)
        {
            this._logger = logger;
        }

        #endregion

        #region Interface Implementations

        public Task Execute(IJobExecutionContext context)
        {
            this._logger.LogInformation("Hello world!");
            return Task.CompletedTask;
        }

        #endregion
    }
}