namespace Boilerplate.Services
{
    #region << Using >>

    using System.Threading.Tasks;
    using Boilerplate.Models;

    #endregion

    public interface IItemService
    {
        Task<Item> AddOrUpdate(ItemDto itemDto);

        Task Delete(int id);

        Task<Item[]> GetAsync(int? id);
    }
}