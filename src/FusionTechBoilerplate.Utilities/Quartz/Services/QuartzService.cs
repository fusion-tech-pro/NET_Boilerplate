namespace FusionTechBoilerplate.Utilities
{
    #region << Using >>

    using Quartz;
    using Quartz.Impl;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public class QuartzService
    {
        #region Constants

        private const string triggerPostfix = "trigger";

        private const string nullSchedulerExceptionMessage = "Scheduler have to be initialized!";

        #endregion

        #region Properties

        private static Lazy<ConcurrentDictionary<Type, List<QuartzSchedule>>> Schedules { get; } = new Lazy<ConcurrentDictionary<Type, List<QuartzSchedule>>>();

        private readonly IScheduler _scheduler;

        #endregion

        #region Constructors

        public QuartzService(IServiceProvider serviceProvider)
        {
            var schedulerFactory = new StdSchedulerFactory();

            this._scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
            this._scheduler.JobFactory = new SingletonJobFactory(serviceProvider);
            this._scheduler.Start().GetAwaiter().GetResult();
        }

        #endregion

        #region Interface Implementations

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            if (this._scheduler == null)
                throw new NullReferenceException(nullSchedulerExceptionMessage);

            if (!this._scheduler.IsStarted)
                await this._scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            if (this._scheduler == null)
                throw new NullReferenceException(nullSchedulerExceptionMessage);

            if (this._scheduler.IsStarted)
                await this._scheduler?.Shutdown(cancellationToken);
        }

        #endregion

        public async Task<QuartzSchedule> ScheduleJob<T>(string cronExpression = default, CancellationToken cancellationToken = default) where T : IJob
        {
            if (this._scheduler == null)
                throw new NullReferenceException(nullSchedulerExceptionMessage);

            var group = $"{Guid.NewGuid():N}";
            var jobType = typeof(T);
            var jobIdentity = $"{jobType.FullName}-{group}";
            var job = createJob<T>(jobIdentity, group);
            var trigger = createTrigger(jobIdentity, group, cronExpression);

            if (!Schedules.Value.ContainsKey(jobType))
                Schedules.Value.GetOrAdd(jobType, new List<QuartzSchedule>());

            var quartzSchedule = new QuartzSchedule(job, trigger, new CancellationToken());

            Schedules.Value[jobType].Add(quartzSchedule);

            await this._scheduler.ScheduleJob(job, trigger, cancellationToken);

            return quartzSchedule;
        }

        public List<QuartzSchedule> GetScheduledJobs<T>() where T : IJob
        {
            return !Schedules.Value.ContainsKey(typeof(T)) ? new List<QuartzSchedule>() : Schedules.Value[typeof(T)];
        }

        private IJobDetail createJob<T>(string name, string group) where T : IJob
        {
            var jobType = typeof(T);

            return JobBuilder
                   .Create(jobType)
                   .WithIdentity(new JobKey(name, group))
                   .WithDescription(jobType.Name)
                   .Build();
        }

        private ITrigger createTrigger(string jobName, string group, string cronSchedule = default)
        {
            var triggerBuilder = TriggerBuilder
                                 .Create()
                                 .WithIdentity(new TriggerKey($"{jobName}-{triggerPostfix}", group));

            if (!string.IsNullOrWhiteSpace(cronSchedule))
                triggerBuilder.WithCronSchedule(cronSchedule)
                              .WithDescription(cronSchedule);
            else
                triggerBuilder.StartNow();

            return triggerBuilder.Build();
        }
    }
}
