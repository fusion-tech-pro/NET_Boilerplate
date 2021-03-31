namespace Boilerplate.API
{
    #region << Using >>

    using System;
    using System.Threading.Tasks;
    using Boilerplate.Services;
    using FusionTechBoilerplate.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    #endregion

    public class ItemController : Controller
    {
        #region Properties

        private readonly IItemService _itemService;

        private readonly ILogger<ItemController> _logger;

        #endregion

        #region Constructors

        public ItemController(IItemService itemService,
                              ILogger<ItemController> logger)
        {
            this._logger = logger;
            this._itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            this._logger.LogInformation("Test Item Controller..");
            var item = await this._itemService.GetAsync(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemDto itemDto)
        {
            await this._itemService.AddOrUpdate(itemDto);
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
            await this._itemService.AddOrUpdate(item);
            return Ok("ok");
        }
    }
}