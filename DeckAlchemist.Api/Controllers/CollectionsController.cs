using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Api.Sources.Collection;
using DeckAlchemist.Api.Sources.Cards.Mtg;
using DeckAlchemist.Api.Sources.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DeckAlchemist.Api.Contracts;

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
                var uId = Utility.UserInfo.Id(HttpContext.User);
                var userEmail = Utility.UserInfo.Email(HttpContext.User);
                var result = _collectionSource.GetCollection(uId);
                if (result!=null) return Json(result);
                return StatusCode(500);
            }catch (Exception)
            {
                return StatusCode(500);
            }
        } 

        //add one or many cards
        [HttpPut("cards")]
        public IActionResult AddCardsToCollection([FromBody]IList<string> cardnames)
        {
            try
            {
                var uId = Utility.UserInfo.Id(HttpContext.User);
                var userEmail = Utility.UserInfo.Email(HttpContext.User);
                var cardExists = _cardSource.CheckExistance(cardnames);
                if (!cardExists) return StatusCode(401);
                var result = _collectionSource.AddCardToCollection(uId, cardnames);
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
                var uId = Utility.UserInfo.Id(HttpContext.User);
                var userEmail = Utility.UserInfo.Email(HttpContext.User);
                var cardExists = _cardSource.CheckExistance(cardnames);
                if (!cardExists) return StatusCode(401);
                var result = _collectionSource.RemoveCardFromCollection(uId, cardnames);
                if (result) return StatusCode(200);
                return StatusCode(500);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        //lend one ore menay cards
        [HttpPost("lend")]
        public IActionResult LendcardsTo([FromBody] LendContract lendContract){
            try
            {
                var uId = Utility.UserInfo.Id(HttpContext.User);
                var userEmail = Utility.UserInfo.Email(HttpContext.User);
                var reciverExists = _userSource.UserExists(lendContract.Lender);
                if (!reciverExists) return StatusCode(401);
                var markAsLent = _collectionSource.MarkCardAsLent(uId, lendContract.Lendee, lendContract.CardsAndAmounts);
                var uIdOfRevicer = lendContract.Lendee;
                var reciveCard = _collectionSource.AddCardAsLent(uId, lendContract.Lendee, lendContract.CardsAndAmounts);
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
