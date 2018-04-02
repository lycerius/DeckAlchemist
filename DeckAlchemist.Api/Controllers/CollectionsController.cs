using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Api.Sources.Collection;
using DeckAlchemist.Api.Sources.Cards.Mtg;
using DeckAlchemist.Api.Sources.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
    [Route("api/collection")]
    public class CollectionsController : Controller
    {
        readonly ICollectionSource _collectionSource;
        readonly IMtgCardSource _cardSource;
        readonly IUserSource _userSource;

        public CollectionsController(ICollectionSource collectionSource, IMtgCardSource cardSource, IUserSource userSource)
        {
            _collectionSource = collectionSource;
            _cardSource = cardSource;
            _userSource = userSource;
        }
        [HttpGet]
        public IActionResult GetCollection(){
            try
            {
                var uId = Auth.UserInfo.Id(HttpContext.User);
                var userEmail = Auth.UserInfo.Email(HttpContext.User);
                Support.Objects.Collection.ICollection result = _collectionSource.GetCollection(uId);
                if (result!=null) return Json(result);
                return StatusCode(500);
            }catch (Exception)
            {
                return StatusCode(500);
            }
        } 

        //add one or many cards
        [HttpPut("cards")]
        public IActionResult AddCardToCollection([FromBody]IList<string> cardnames)
        {
            try
            {
                var uId = Auth.UserInfo.Id(HttpContext.User);
                var userEmail = Auth.UserInfo.Email(HttpContext.User);
                bool cardExists = _cardSource.CheckExistance(cardnames);
                if (!cardExists) return StatusCode(401);
                bool result = _collectionSource.AddCardToCollection(uId, cardnames);
                if (result) return StatusCode(200);
                return StatusCode(500);
            }
            catch(Exception){
                return StatusCode(500);
            }
        }

        //remove one or many cards
        [HttpDelete("cards")]
        public IActionResult RemoveCardsFromCollection([FromBody]string[] cardnames)
        {
            try
            {
                var uId = Auth.UserInfo.Id(HttpContext.User);
                var userEmail = Auth.UserInfo.Email(HttpContext.User);
                bool cardExists = _cardSource.CheckExistance(cardnames);
                if (!cardExists) return StatusCode(401);
                bool result = _collectionSource.RemoveCardFromCollection(uId, cardnames);
                if (result) return StatusCode(200);
                return StatusCode(500);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        //lend one ore menay cards
        [HttpPost("cards")]
        public IActionResult LendcardsTo([FromBody] string reciver, string[] cardsnames){
            try
            {
                var uId = Auth.UserInfo.Id(HttpContext.User);
                var userEmail = Auth.UserInfo.Email(HttpContext.User);
                bool reciverExists = _userSource.UserExists(reciver);
                if (!reciverExists) return StatusCode(401);
                bool markAsLent = _collectionSource.MarkCardAsLent(uId, cardsnames);
                var uIdOfRevicer = _userSource.GetUIDByName(reciver);
                bool reciveCard = _collectionSource.AddCardAsLent(uIdOfRevicer,cardsnames);
                if (markAsLent && reciveCard) return StatusCode(200);
                return StatusCode(500);

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        } 
    }
}
