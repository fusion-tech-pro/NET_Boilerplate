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
    using FusionTechBoilerplate.Authentication;

    #endregion

    [Injectable(ServiceLifetime.Scoped)]
    [UsedImplicitly]
    public class ItemService : IItemService
    {
        #region Properties

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IAuthService _authService;

        #endregion

        #region Constructors

        public ItemService(IUnitOfWork unitOfWork, 
            IMapper mapper,
            IAuthService authService)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._authService = authService;
        }

        #endregion

        #region Interface Implementations

        public async Task Delete(int id)
        {
            var userId = _authService.GetCurretUserId();  
            var specUser = new FindItemByUserIdSpec<Item>(userId);

            var spec = (Specification<Item>)specUser.And(new FindByIdSpec<Item>(id));
            var item = await this._unitOfWork.Repository<Item>().Get(spec).SingleOrDefaultAsync();

            if (item != null && item.User.Id != userId) {
                throw new EntityNotFoundException(nameof(User), userId);
            }

            this._unitOfWork.Repository<Item>().Delete(item);
            await this._unitOfWork.SaveChangesAsync();
        }

        public async Task<ItemDto[]> GetAsync(int? id)
        {
            var specUser = new FindItemByUserIdSpec<Item>(_authService.GetCurretUserId());  

            var spec = (Specification<Item>)(id.HasValue ? specUser.And(new FindByIdSpec<Item>(id.Value)) : specUser);
            var items = await this._unitOfWork.Repository<Item>().Get(spec).ToArrayAsync();

            if (id.HasValue && items.All(i => i.Id != id))
                throw new EntityNotFoundException(nameof(items), id);

            return this._mapper.Map<ItemDto[]>(items.OrderBy(v => v.Value));
        }

        public async Task<ItemDto> AddOrUpdate(ItemDto itemDto)
        {
            var isNew = false;

            var userId = _authService.GetCurretUserId();  
            var specUser = new FindUserByIdSpec(userId);
            var currentUser = await this._unitOfWork.Repository<User>().Get(specUser).SingleOrDefaultAsync();

            if (currentUser == null)
                throw new EntityNotFoundException(nameof(currentUser), userId);

            var spec = new FindByIdSpec<Item>(itemDto.Id.GetValueOrDefault());
            var item = await this._unitOfWork.Repository<Item>().Get(spec).SingleOrDefaultAsync();

            if (item == null && itemDto.Id.HasValue)
                throw new EntityNotFoundException(nameof(item), itemDto.Id);

            if (item != null && item.User != currentUser) {
                throw new EntityNotFoundException(nameof(currentUser), userId);
            }

            if (item == null)
            {
                isNew = true;
                item = new Item { User = currentUser };
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
            var userId = _authService.GetCurretUserId();  
            var specUser = new FindItemByUserIdSpec<Item>(userId);

            var spec = (Specification<Item>)specUser.And(new FindByIdSpec<Item>(id));
            var item = await this._unitOfWork.Repository<Item>().Get(spec).SingleOrDefaultAsync();

            if (item != null && item.User.Id != userId) {
                throw new EntityNotFoundException(nameof(User), userId);
            }

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