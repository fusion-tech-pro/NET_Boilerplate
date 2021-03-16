namespace Boilerplate.Services
{
    #region << Using >>

    using System.Threading.Tasks;
    using Boilerplate.Domain;
    using Boilerplate.Models;
    using Microsoft.EntityFrameworkCore;
    using Quartz;

    #endregion

    public class TestJob : IJob
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        public TestJob(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Interface Implementations

        public async Task Execute(IJobExecutionContext context)
        {
            var key = context.JobDetail.Key;
            var id = 7;
            var spec = new FindByIdSpec<Item>(id);
            var items = await this._unitOfWork.Repository<Item>().Get(spec).ToArrayAsync();
        }

        #endregion
    }
}