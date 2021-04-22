namespace FusionTechBoilerplate.Services
{
    #region << Using >>

    using System;
    using System.Threading.Tasks;
    using System.Transactions;
    using AutoMapper;
    using FusionTechBoilerplate.Domain;
    using FusionTechBoilerplate.Models;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    #endregion

    [Injectable(ServiceLifetime.Scoped)]
    [UsedImplicitly]
    public class ItemService : IItemService
    {
        #region Properties

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        private readonly AppDbContext db;

        #endregion

        #region Constructors

        public ItemService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this.db = context;
        }

        #endregion

        #region Interface Implementations

        public async Task Delete(int id)
        {
            var spec = new FindByIdSpec<Item>(id);
            var item = await this._unitOfWork.Repository<Item>().Get(spec).SingleOrDefaultAsync();

            this._unitOfWork.Repository<Item>().Delete(item);
            await this._unitOfWork.SaveChangesAsync();
        }

        public async Task<ItemDto[]> GetAsync(int? id)
        {
            var spec = id.HasValue ? new FindByIdSpec<Item>(id.Value) : null;
            var items = await this._unitOfWork.Repository<Item>().Get(spec).ToArrayAsync();

            if (items == null && id.HasValue)
                throw new EntityNotFoundException(nameof(items), id);

            return this._mapper.Map<ItemDto[]>(items);
        }

        public async Task TestTransaction()
        {
            using (var scopeOne = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled)) //options need to see the documentation
            {
                this._unitOfWork.BeginTransactionAsync(async () =>
                                                       {
                                                           await AddOrUpdate(new ItemDto()
                                                                             {
                                                                                     Status = Status.New,
                                                                                     Value = "OneTestObject"
                                                                             });
                                                       });
                using (var scopeTwo = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    this._unitOfWork.BeginTransactionAsync(async () =>
                                                           {
                                                               await AddOrUpdate(new ItemDto()
                                                                                 {
                                                                                         Status = Status.New,
                                                                                         Value = "TwoTestObject"
                                                                                 });
                                                           });
                    //throw new Exception("level two");
                    using (var scopedThree = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                    {
                        this._unitOfWork.BeginTransactionAsync(async () =>
                                                               {
                                                                   await AddOrUpdate(new ItemDto()
                                                                                     {
                                                                                             Status = Status.New,
                                                                                             Value = "ThreeTestObject"
                                                                                     });
                                                               });
                        // throw new Exception("level three");
                        scopedThree.Complete();
                    }
                    scopeTwo.Complete();
                }

                //throw new Exception("level one");
                scopeOne.Complete();
            }
        }

        public async Task<ItemDto> AddOrUpdate(ItemDto itemDto)
        {
            var isNew = false;

            var spec = new FindByIdSpec<Item>(itemDto.Id.GetValueOrDefault());
            var item = await this._unitOfWork.Repository<Item>().Get(spec).SingleOrDefaultAsync();

            if (item == null && itemDto.Id.HasValue)
                throw new EntityNotFoundException(nameof(item), itemDto.Id);

            if (itemDto.Id == null)
            {
                isNew = true;
                item = new Item();
            }

            item.Status = itemDto.Status;
            item.UpdateDate = DateTime.UtcNow;
            item.Value = itemDto.Value;

            if (isNew)
                this._unitOfWork.Repository<Item>().Add(item);
            else
                this._unitOfWork.Repository<Item>().Update(item);

            await this._unitOfWork.SaveChangesAsync(true);

            return this._mapper.Map<ItemDto>(item);
        }

        #endregion
    }
}