namespace FusionTechBoilerplate.Services
{
    #region << Using >>

    using System.Threading.Tasks;
    using FusionTechBoilerplate.Models;

    #endregion

    public interface IItemService
    {
        Task<ItemDto> AddOrUpdate(ItemDto itemDto);

        Task<ItemDto> ChangeStatus(int id);

        Task Delete(int id);

        Task<ItemDto[]> GetAsync(int? id);
    }
}