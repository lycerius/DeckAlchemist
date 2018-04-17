using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DeckAlchemist.WebApp.Models;

namespace DeckAlchemist.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {   
            return View("Home");
        }

        public IActionResult Decks()
        {
            return View("Decks");
        }

        public IActionResult Meta()
        {
            return View("Meta");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
