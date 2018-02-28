using System.Collections.Generic;
using DeckAlchemist.Api.Sources.UserDeck;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
    [Route("api/[controller]")]
    public class UserDeckController : Controller
    {

        readonly IUserDeckSource _source;

        public UserDeckController(IUserDeckSource source)
        {
            _source = source;
        }
    }
}
