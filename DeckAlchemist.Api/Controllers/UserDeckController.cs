using System.Collections.Generic;
using DeckAlchemist.Api.Sources.UserDeck;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
    [Route("api/UserDeck")]
    public class UserDeckController : Controller
    {

        readonly IUserDeckSource _source;

        public UserDeckController(IUserDeckSource source)
        {
            _source = source;
        }
        [HttpGet("deck")]
        public IActionResult GetDeckByName([FromBody] string deckName)
        {
            var uId = Utility.UserInfo.Id(HttpContext.User);
            var email = Utility.UserInfo.Email(HttpContext.User);
            var result  = _source.GetDeckByName(uId, deckName);
            throw new System.NotImplementedException();
        }
        [HttpPut("deck")]
        public IActionResult CreateDeck([FromBody] string deckName)
        {
            var uId = Utility.UserInfo.Id(HttpContext.User);
            var email = Utility.UserInfo.Email(HttpContext.User);
            bool result = _source.CreateDeck(uId, deckName);
            if (!result) return StatusCode(500);
            return StatusCode(200);
        }
        [HttpDelete("deck")]
        public IActionResult DeleteDeck([FromBody] string deckName){
            var uId = Utility.UserInfo.Id(HttpContext.User);
            var email = Utility.UserInfo.Email(HttpContext.User);
            bool result = _source.DeleteDeck(uId, deckName);
            if (!result) return StatusCode(500);
            return StatusCode(200);
        }

        [HttpPut("deck/card")]
        public IActionResult AddCardToDeck([FromBody] string deckName, [FromBody] string cardName)
        {
            var uId = Utility.UserInfo.Id(HttpContext.User);
            var email = Utility.UserInfo.Email(HttpContext.User);
            bool result = _source.AddCardToDeck(uId, deckName,cardName);
            if (!result) return StatusCode(500);
            return StatusCode(200);
        }
        [HttpDelete("deck/card")]
        public IActionResult RemoveCardfromDeck([FromBody] string deckName, [FromBody] string cardName)
        {
            var uId = Utility.UserInfo.Id(HttpContext.User);
            var email = Utility.UserInfo.Email(HttpContext.User);
            bool result = _source.RemoveCardFromDeck(uId, deckName, cardName);
            if (!result) return StatusCode(500);
            return StatusCode(200);
        }

    }
}
