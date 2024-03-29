﻿namespace FusionTechBoilerplate.Utilities
{
    #region << Using >>

    using Quartz;
    using System;
    using System.Threading;

    #endregion

    public class QuartzSchedule
    {
        #region Properties

        public IJobDetail Job { get; }

        public ITrigger Trigger { get; }

        public CancellationToken CancellationToken { get; }

        public DateTime CreateDate { get; }

        #endregion

        #region Constructors

        public QuartzSchedule(IJobDetail job, ITrigger trigger, CancellationToken cancellationToken)
        {
            Job = job;
            Trigger = trigger;
            CancellationToken = cancellationToken;
            CreateDate = DateTime.UtcNow;
        }

        #endregion
    }
}
