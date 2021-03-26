namespace Boilerplate.Domain
{
    #region << Using >>

    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Quartz;

    #endregion

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