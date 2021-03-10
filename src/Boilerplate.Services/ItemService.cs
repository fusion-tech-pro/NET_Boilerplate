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

        //private readonly IRepository<Item> _repo;

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        public ItemService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Interface Implementations

        public Task Add(ItemAddDto itemDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ItemAddDto, Item>()
               .ForMember("Value", opt => opt.MapFrom(c => c.Value))
               .ForMember("Status", opt => opt.MapFrom(c => c.Status))
               .ForMember("CreateDate", opt => opt.MapFrom(c => c.CreateDate)));

            var mapper = new Mapper(config);
            var item = mapper.Map<ItemAddDto, Item>(itemDto);

            this._unitOfWork.Repository<Item>().Add(item);
            return this._unitOfWork.Repository<Item>().SaveChangesAsync();
        }

        public Task Update(ItemUpdateDto itemDto)
        {
            var item = this._unitOfWork.Repository<Item>().Get(itemDto.Id);

            if (item == null)
                throw new ArgumentNullException();

            item.Status = itemDto.Status;
            item.UpdateDate = DateTime.UtcNow;
            item.Value = itemDto.Value;

            this._unitOfWork.Repository<Item>().Update(item);
            return this._unitOfWork.Repository<Item>().SaveChangesAsync();
        }

        public  Task Put(ItemUpdateDto itemDto)
        {
            var item =  this._unitOfWork.Repository<Item>().Get(itemDto.Id);

            if (item == null)
                throw new ArgumentNullException();

            item.Status = itemDto.Status;
            item.UpdateDate = DateTime.UtcNow;
            item.Value = itemDto.Value;

            this._unitOfWork.Repository<Item>().Update(item);
            return this._unitOfWork.Repository<Item>().SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var item = this._unitOfWork.Repository<Item>().Get(id);

            if (item == null)
                throw new ArgumentNullException();

            this._unitOfWork.Repository<Item>().Delete(item);
            return this._unitOfWork.Repository<Item>().SaveChangesAsync();
        }

        #endregion
    }
}