using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.ViewModel;
using System.Diagnostics;

namespace Project.Controllers
{
    //[Authorize ]//built in filter to check cookie
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult calen()
        {
            return View();
        }
        [HttpPost]
        public IActionResult calen(calenviewmodel c)
        {
            return View();
        }
        public IActionResult Index()
        {
           // string name = User.Identity.Name;

            return View();
        }
        // to allow any one see this action
        [AllowAnonymous]
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