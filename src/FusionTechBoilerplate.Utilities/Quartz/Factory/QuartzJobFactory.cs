namespace FusionTechBoilerplate.Utilities
{
    #region << Using >>

    using Quartz;
    using Quartz.Spi;
    using System;

    #endregion

    class QuartzJobFactory : IJobFactory
    {
        #region Properties

        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Constructors

        public QuartzJobFactory(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        #endregion

        #region Interface Implementations

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;

            var job = (IJob)this._serviceProvider.GetService(jobDetail.JobType);
            return job;
        }

        public void ReturnJob(IJob job) { }

        #endregion
    }
}
