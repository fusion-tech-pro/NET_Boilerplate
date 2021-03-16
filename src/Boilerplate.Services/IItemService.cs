﻿namespace Boilerplate.Services
{
    #region << Using >>

    using System.Threading.Tasks;
    using Boilerplate.Models;

    #endregion

    public interface IItemService
    {
        Task<ItemDto> AddOrUpdate(ItemDto itemDto);

        Task Delete(int id);

        Task<ItemDto[]> GetAsync(int? id);

        Task BackgroundTask();
    }
}