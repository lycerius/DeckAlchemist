using System.Diagnostics;
using DeckAlchemist.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeckAlchemist.WebApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult CreateAccount()
        {
            return View("CreateAccount");
        }

        public IActionResult Logout()
        {
            return View("Logout");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}