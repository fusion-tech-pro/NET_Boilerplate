﻿namespace FusionTechBoilerplate.Services
{
    #region << Using >>

    using System.Threading.Tasks;
    using FusionTechBoilerplate.Models;

    #endregion

    public interface IItemService
    {
        Task<ItemDto> AddOrUpdate(ItemDto itemDto);

        Task Delete(int id);

        Task<ItemDto[]> GetAsync(int? id);

        Task TestTransaction();
    }
}