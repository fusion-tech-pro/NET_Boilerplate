namespace FusionTechBoilerplate.API
{
    #region << Using >>

    using System.Threading.Tasks;
    using FusionTechBoilerplate.Services;
    using FusionTechBoilerplate.Utilities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    #endregion

    [Route("api/quartz")]
    [ApiController]
    public class QuartzController : Controller
    {
        #region Properties

        private readonly ILogger<ItemController> _logger;

        private readonly QuartzService _quartzService;

        #endregion

        #region Constructors

        public QuartzController(ILogger<ItemController> logger,
                                QuartzService quartzService)
        {
            this._logger = logger;
            this._quartzService = quartzService;
        }

        #endregion

        [HttpGet("get")]
        public async Task Get()
        {
            await this._quartzService.StartAsync();
            await this._quartzService.ScheduleJob<TestJob>();

            var scheduledJobs = this._quartzService.GetScheduledJobs<TestJob>();
        }
    }
}