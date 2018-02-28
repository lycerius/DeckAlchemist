using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Support.Objects.User;
using DeckAlchemist.Api.Sources.User;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Route("api/user")]
    public class UsersController : Controller
    {

        readonly IUserSource _source;

        public UsersController(IUserSource source)
        {
            _source = source;
        }
    }
}
