    using BW_U_1.Models;
using BW_U_1.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using 
//testeststest
namespace BW_U_1.Controllers
{
    public class HomeController : Controller
    {
        List<Products> products = new List<Products>();

        private readonly ILogger<HomeController> _logger;
        private readonly IService _service;


        public HomeController(ILogger<HomeController> logger, IService service)
        {
            _logger = logger;
            _service = service;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            products = _service.GetCallAll();
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
