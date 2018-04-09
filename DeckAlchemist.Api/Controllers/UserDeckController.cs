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

        [HttpGet("all")]
        public IActionResult GetAllUserDecks(){
            var uId = Utility.UserInfo.Id(HttpContext.User);
            var email = Utility.UserInfo.Email(HttpContext.User);
            var result = _source.GetAll(uId);
            return Json(result);
        }

        [HttpGet("deck")]
        public IActionResult GetDeckByName([FromBody] string deckName)
        {
            var uId = Utility.UserInfo.Id(HttpContext.User);
            var email = Utility.UserInfo.Email(HttpContext.User);
            var result  = _source.GetDeckByName(uId, deckName);
            return Json(result);
        }
        [HttpPut("deck")]
        public IActionResult CreateDeck([FromBody] string deckName)
        {
            var uId = Utility.UserInfo.Id(HttpContext.User);
            var email = Utility.UserInfo.Email(HttpContext.User);
            var result = _source.CreateDeck(uId, deckName);
            if (!result) return StatusCode(500);
            return StatusCode(200);
        }
        [HttpDelete("deck")]
        public IActionResult DeleteDeck([FromBody] string deckName){
            var uId = Utility.UserInfo.Id(HttpContext.User);
            var email = Utility.UserInfo.Email(HttpContext.User);
            var result = _source.DeleteDeck(uId, deckName);
            if (!result) return StatusCode(500);
            return StatusCode(200);
        }

        [HttpPut("deck/card")]
        public IActionResult AddCardToDeck([FromBody] PutDeckRequest request)
        {
            var uId = Utility.UserInfo.Id(HttpContext.User);
            var email = Utility.UserInfo.Email(HttpContext.User);
            var result = _source.AddCardToDeck(uId, request.DeckName,request.CardName);
            if (!result) return StatusCode(500);
            return StatusCode(200);
        }
        [HttpDelete("deck/card")]
        public IActionResult RemoveCardfromDeck([FromBody] PutDeckRequest request)
        {
            var uId = Utility.UserInfo.Id(HttpContext.User);
            var email = Utility.UserInfo.Email(HttpContext.User);
            var result = _source.RemoveCardFromDeck(uId, request.DeckName,request.CardName);
            if (!result) return StatusCode(500);
            return StatusCode(200);
        }
        
        public class PutDeckRequest {
            public string DeckName { get; set; }
            public string CardName { get; set; }
        }

    }
}
