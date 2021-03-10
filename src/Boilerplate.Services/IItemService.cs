namespace Boilerplate.Services
{
    using System.Threading.Tasks;
    using Boilerplate.Models;

    public interface IItemService
    {
        Task Add(ItemAddDto itemDto);

        Task Update(ItemUpdateDto itemDto);

        Task Put(ItemUpdateDto itemDto);
        
        Task Delete(int id);
    }
}