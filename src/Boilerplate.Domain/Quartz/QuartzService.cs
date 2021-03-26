namespace Boilerplate.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Quartz;
    using Quartz.Spi;

    #endregion

    public class QuartzService : IHostedService
    {
        #region Properties

        public IScheduler Scheduler { get; set; }

        private readonly IJobFactory _jobFactory;

        private readonly IEnumerable<JobSchedule> _jobSchedules;

        private readonly ISchedulerFactory _schedulerFactory;

        #endregion

        #region Constructors

        public QuartzService(ISchedulerFactory schedulerFactory,
                             IJobFactory jobFactory,
                             IEnumerable<JobSchedule> jobSchedules)
        {
            this._schedulerFactory = schedulerFactory;
            this._jobFactory = jobFactory;
            this._jobSchedules = jobSchedules;
        }

        #endregion

        #region Interface Implementations

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await this._schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = this._jobFactory;

            foreach (var jobSchedule in this._jobSchedules)
            {
                var job = CreateJob(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);

                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }

            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        #endregion

        private static IJobDetail CreateJob(JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                   .Create(jobType)
                   .WithIdentity(jobType.FullName)
                   .WithDescription(jobType.Name)
                   .Build();
        }

        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            return TriggerBuilder
                   .Create()
                   .WithIdentity($"{schedule.JobType.FullName}.trigger")
                   .WithCronSchedule(schedule.CronExpression)
                   .WithDescription(schedule.CronExpression)
                   .Build();
        }
    }
}