﻿namespace Boilerplate.API
{
    #region << Using >>

    using System;
    using System.Threading.Tasks;
    using Boilerplate.Models;
    using Boilerplate.Services;
    using Microsoft.AspNetCore.Mvc;

    #endregion

    public class ItemController : Controller
    {
        #region Properties

        private readonly IItemService _itemService;

        #endregion

        #region Constructors

        public ItemController(IItemService itemService)
        {
            this._itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            var item = await this._itemService.GetAsync(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            await this._itemService.AddOrUpdate(itemDto);
            return Ok("ok");
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ItemDto item)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            await this._itemService.AddOrUpdate(item);
            return Ok("ok");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await this._itemService.Delete(id);
            return Ok("ok");
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ItemDto item)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            await this._itemService.AddOrUpdate(item);
            return Ok("ok");
        }
    }
}