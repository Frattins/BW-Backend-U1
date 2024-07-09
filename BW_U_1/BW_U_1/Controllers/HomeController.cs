using System.Diagnostics;
using BW_U_1.Models;
using BW_U_1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BW_U_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService _service;

        //COSTRUTTORE
        public HomeController(ILogger<HomeController> logger, IService service)
        {
            _logger = logger;
            _service = service;
        }
        // ***************

        //PRIMA PAGINA "HOME"
        public IActionResult Index()
        {
            return View();
        }

        // ***************

        //PAGINA CON TUTTE LE BIRRE
        public IActionResult All()
        {
            var products = _service.GetCallAll();
            return View(products);
        }

        //Funzioni annesse
        //Delete
        [HttpPost]
        public IActionResult ButtonElimina(int id)
        {
            _service.DeleteCall(id);

            return RedirectToAction("All");
            
        }
        // ***************

        //FORM
        public IActionResult Form()
        {
            var Prodotto = new Products();
            return View(Prodotto); 
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
