namespace FusionTechBoilerplate.Services
{
    #region << Using >>

    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using System.Linq;
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

        #endregion

        #region Constructors

        public ItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
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

            if (id.HasValue && items.All(i => i.Id != id))
                throw new EntityNotFoundException(nameof(items), id);

            return this._mapper.Map<ItemDto[]>(items.OrderBy(v => v.Value));
        }

        public async Task<ItemDto> AddOrUpdate(ItemDto itemDto)
        {
            var isNew = false;

            var spec = new FindByIdSpec<Item>(itemDto.Id.GetValueOrDefault());
            var item = await this._unitOfWork.Repository<Item>().Get(spec).SingleOrDefaultAsync();

            if (item == null && itemDto.Id.HasValue)
                throw new EntityNotFoundException(nameof(item), itemDto.Id);

            if (item == null)
            {
                isNew = true;
                item = new Item();
            }

            item.Status = Status.New;
            item.UpdateDate = DateTime.UtcNow;
            item.Value = itemDto.Value;

            if (isNew)
                this._unitOfWork.Repository<Item>().Add(item);
            else
                this._unitOfWork.Repository<Item>().Update(item);

            await this._unitOfWork.SaveChangesAsync();

            return this._mapper.Map<ItemDto>(item);
        }

        public async Task<ItemDto> ChangeStatus(int id)
        {
            var spec = new FindByIdSpec<Item>(id);
            var item = await this._unitOfWork.Repository<Item>().Get(spec).SingleOrDefaultAsync();

            switch (item.Status)
            {
                case Status.New:
                case Status.Done:
                    item.Status = Status.InProgress;
                    break;

                case Status.InProgress:
                    item.Status = Status.Done;
                    break;

                default:
                    throw new NotImplementedException();
            };

            await this._unitOfWork.SaveChangesAsync();
            return this._mapper.Map<ItemDto>(item);
        }

        #endregion
    }
}