using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DeckAlchemist.WebApp.Api.Controllers {

    [Route("api/test")]
    public class TestApiController : Controller {

        [HttpGet("ping")]
        public bool ping() {
            return true;
        }


    }

}