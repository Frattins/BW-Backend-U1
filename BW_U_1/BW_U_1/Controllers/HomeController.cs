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
        private readonly ICarts _serviceCart;

        //VALORI
        int CarrelloID = 0;

        //COSTRUTTORE
        public HomeController(ILogger<HomeController> logger, IService service, ICarts serviceCart)
        {
            _logger = logger;
            _service = service;
            _serviceCart = serviceCart;
        }
        // ***************

        //PRIMA PAGINA "HOME"
        public IActionResult Index()
        {
            var products = _service.GetCallAll();
            return View(products);
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

        //DETTAGLI
        public IActionResult ProductDetails(int id)
        {
            var product = _service.GetCallOneID(id);
            return View(product);
        }
        // **********************************************

        //FORM
        public IActionResult Form()
        {
            return View(); 
        }
        // bottone del form
        [HttpPost]
        public IActionResult BottoneForm(Products prodotto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.WriteCall(prodotto);
                    return RedirectToAction("All");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore durante l'inserimento del prodotto: " + ex.Message);
                }
            }
            return View(prodotto); // In caso di errore, ritorna alla view con i dati immessi
        }

        // **********************************************

        //MODIFICA IL FORM
        public IActionResult FormModifica(int ID) 
        {
           var prodotto = _service.GetCallOneID(ID);
            return View(prodotto);
        }

        [HttpPost]
        public IActionResult FormUpdate(int ID, Products prodotto)
        {
            _service.UpdateCall(prodotto.IdProd, prodotto);
            return RedirectToAction("All");
        }

        // **********************************************

        //INTERAZIONI CON CARRELLO:
        // TUTTI I CARRELLI
        public IActionResult AllCarrelli() 
        {
            var carts = _serviceCart.GetAllCarts();
            return View(carts); 
        }

        //SCEGLI CARRELLO
        public IActionResult ScegliCarrello(int IdCart) 
        {
            TempData["CarrelloID"] = IdCart;
            return RedirectToAction("All");
        }

        // AGGIUNGI PRODOTTO AL CARRELLO

        public IActionResult AggiungiAdCart(int IdProd, int CarrelloID)
        {
            int IdCarr = Convert.ToInt32(TempData["CarrelloID"]);
            if (IdCarr != 0)
            {
                _serviceCart.AddCart(IdProd, IdCarr);
                return RedirectToAction("All");
            }
            else
            {
                return RedirectToAction("AllCarrelli");
            }
        }

        //CREA CARRELLO
        public IActionResult CreaCart() 
        {
            _serviceCart.CreaCart();
            return RedirectToAction("AllCarrelli");
        }  

        //ELIMINA CARRELLO
        [HttpPost]
        public IActionResult EliminaCarrello(int IdCart) 
        {
            Console.WriteLine($"ID ricevuto: {IdCart}");
            _serviceCart.DeleteCart(IdCart);

            return RedirectToAction("AllCarrelli");
        }

        // **********************************************

        //DETTAGLI CARRELLO
        public IActionResult DeteilsCart(int IdCart)
        {
            var dettagli = _serviceCart.DeteilsCart(IdCart).ToList();
            var total = dettagli.Sum(item => item.Price * item.quantita);
            ViewBag.CartID = IdCart; 
            var viewModel = new CartDetailsViewModel
            {
                Items = dettagli,
                Total = total
            };

            return View(viewModel);
        }

        //AGGIUNGI PRODOTTO CARRELLO
        public IActionResult AggiungiItems(int IdProd, int IdCart) 
        {
            _serviceCart.AggiungiItem(IdCart, IdProd);
            return RedirectToAction("DeteilsCart", new { IdCart = IdCart });
        }

        //RIMUOVI PRODOTTI
        public IActionResult RimuoviItem(int IdProd, int IdCart)
        {
            _serviceCart.RimuoviItem(IdCart, IdProd);
            return RedirectToAction("DeteilsCart", new { IdCart = IdCart });
        }

        //RITORNA ALLA HOME 
        public IActionResult AtHome()
        {
            return RedirectToAction("All");
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
