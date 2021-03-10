namespace Boilerplate.API
{
    #region << Using >>

    using System.Diagnostics;
    using Boilerplate.API.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    #endregion

    public class HomeController : Controller
    {
        #region Properties

        private readonly ILogger<HomeController> _logger;

        #endregion

        #region Constructors

        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}