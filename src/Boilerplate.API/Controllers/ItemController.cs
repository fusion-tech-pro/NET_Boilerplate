namespace Boilerplate.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Boilerplate.Domain;
    using Boilerplate.Models;
    using Boilerplate.Services;
    using Microsoft.AspNetCore.Mvc;

    public class ItemController : Controller {

        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            this._itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
        }


        [HttpPost]
        public IActionResult Test([FromBody]Item item) {

            if(!ModelState.IsValid) {
                
            }
            
            return Ok("ok");
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ItemAddDto itemDto)
        {
            /*CODEREVIEW: here and beneath remove 'if'-nesting,
             use if(!ModelState.IsValid) return ... instead*/
            if (ModelState.IsValid)
            {
                await this._itemService.Add(itemDto);
            }
            else
            {
                return ValidationProblem(ModelState);
            }

            return Ok("ok");
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ItemUpdateDto item) {

            if (ModelState.IsValid)
            {
                await _itemService.Put(item);
            }
            else
            {
                return ValidationProblem(ModelState);
            }

            return Ok("ok");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {

            if (ModelState.IsValid)
            {
                await _itemService.Delete(id);
            }
            else
            {
                return ValidationProblem(ModelState);
            }

            return Ok("ok");
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ItemUpdateDto item)
        {

            if (ModelState.IsValid)
            {
                await _itemService.Update(item);
            }
            else
            {
                return ValidationProblem(ModelState);
            }

            return Ok("ok");
        }
    }
}
