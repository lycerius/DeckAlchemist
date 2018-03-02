using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Api.Sources.Collection;
using DeckAlchemist.Api.Sources.Cards.Mtg;
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

        public CollectionsController(ICollectionSource collectionSource, IMtgCardSource cardSource)
        {
            _collectionSource = collectionSource;
            _cardSource = cardSource;
        }

        //one or many users
        [HttpGet]
        public void AllCollectionsByUsersIds([FromBody]string[] users)
        {
          
        }

        //one or many cards
        [HttpPut("cards")]
        public IActionResult AddCardsToCollection([FromBody]IEnumerable<string> cardnames)
        {
            try
            {
                var uId = HttpContext.User.FindFirst("sub").Value;
                bool cardExists = _cardSource.CheckExistance(cardnames);
                if (!cardExists) return StatusCode(401);
                bool result = _collectionSource.addCardToCollection(uId, cardnames);
                if (result) return StatusCode(200);
                return StatusCode(500);
            }
            catch(Exception){
                return StatusCode(500);
            }
        }

        //remove one ore many cards
        [HttpDelete("cards")]
        public void RemoveCardsFromCollection([FromBody]string[] cardnames)
        {
     
        }
    }
}
