using Microsoft.AspNetCore.Mvc;

namespace DeckAlchemist.WebApp.Controllers
{
    public class GroupsController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}