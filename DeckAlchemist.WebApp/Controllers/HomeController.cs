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
            //Do auth code
            return View("Login");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
