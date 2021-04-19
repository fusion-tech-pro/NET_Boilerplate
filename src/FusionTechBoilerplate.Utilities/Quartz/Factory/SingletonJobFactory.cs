namespace FusionTechBoilerplate.Utilities
{
    #region << Using >>

    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Quartz;
    using Quartz.Spi;

    #endregion

    public class SingletonJobFactory : IJobFactory
    {
        #region Properties

        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Constructors

        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        #endregion

        #region Interface Implementations

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return this._serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job) { }

        #endregion
    }
}