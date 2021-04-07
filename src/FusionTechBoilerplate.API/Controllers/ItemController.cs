namespace FusionTechBoilerplate.API
{
    #region << Using >>

    using System;
    using System.Threading.Tasks;
    using FusionTechBoilerplate.Models;
    using FusionTechBoilerplate.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    #endregion

    [Route("api/item")]
    [ApiController]
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

        [HttpGet("get")]
        public async Task<IActionResult> Get(int? id)
        {
            this._logger.LogInformation("Test Item Controller..");
            var item = await this._itemService.GetAsync(id);
            return Ok(item);
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] ItemDto itemDto)
        {
            await this._itemService.AddOrUpdate(itemDto);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this._itemService.Delete(id);
            return Ok();
        }

        [HttpPut("put/{id}")]
        public async Task<IActionResult> Put(int id)
        {
            await this._itemService.ChangeStatus(id);
            return Ok();
        }
    }
}