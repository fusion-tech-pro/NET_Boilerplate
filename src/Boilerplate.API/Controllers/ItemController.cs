namespace Boilerplate.API.Controllers
{
    using Boilerplate.Validator;
    using Microsoft.AspNetCore.Mvc;

    public class ItemController : Controller {
    
        [HttpPost]
        public IActionResult Test([FromBody]Item item) {

            if(!ModelState.IsValid) {
                return  ValidationProblem(ModelState);
            }
            
            return Ok("ok");
        }
    }
}
