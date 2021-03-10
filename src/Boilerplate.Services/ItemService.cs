namespace Boilerplate.Services
{
    #region << Using >>

    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Boilerplate.Domain;
    using Boilerplate.Models;

    #endregion

    public class ItemService : IItemService
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        public ItemService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Interface Implementations

        public async Task Delete(int id)
        {
            var item = this._unitOfWork.Repository<Item>().Get(id);

            if (item == null)
                throw new ArgumentNullException();

            this._unitOfWork.Repository<Item>().Delete(item);
            await this._unitOfWork.Repository<Item>().SaveChangesAsync();
        }

        public async Task<Item> AddOrUpdate(ItemDto itemDto)
        {
            Item item;
            if (itemDto.Id.HasValue)
                item = await Update(itemDto);
            else
                item = await Add(itemDto);

            return item;
        }

        #endregion

        private async Task<Item> Add(ItemDto itemDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ItemDto, Item>().ReverseMap());

            var mapper = new Mapper(config);
            var item = mapper.Map<Item>(itemDto);

            this._unitOfWork.Repository<Item>().Add(item);
            await this._unitOfWork.Repository<Item>().SaveChangesAsync();
            return item;
        }

        private async Task<Item> Update(ItemDto itemDto)
        {
            if (!itemDto.Id.HasValue)
                throw new ArgumentNullException();

            var item = this._unitOfWork.Repository<Item>().Get(itemDto.Id.Value);

            if (item == null)
                throw new ArgumentNullException();

            item.Status = itemDto.Status;
            item.UpdateDate = DateTime.UtcNow;
            item.Value = itemDto.Value;

            this._unitOfWork.Repository<Item>().Update(item);
            await this._unitOfWork.Repository<Item>().SaveChangesAsync();
            return item;
        }
    }
}