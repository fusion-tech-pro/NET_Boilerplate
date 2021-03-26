namespace Boilerplate.Domain
{
    #region << Using >>

    using System;

    #endregion

    public class JobSchedule
    {
        #region Properties

        public Type JobType { get; }

        public string CronExpression { get; }

        #endregion

        #region Constructors

        public JobSchedule(Type jobType, string cronExpression)
        {
            JobType = jobType;
            CronExpression = cronExpression;
        }

        #endregion
    }
}