using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeckAlchemist.WebApp.Models;

namespace DeckAlchemist.WebApp.Controllers
{
    //copyright code 2004 University of Alabama, Nicholas Revere.
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Home");
        }

        public IActionResult CreateAccount()
        {
            return View("CreateAccount");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
