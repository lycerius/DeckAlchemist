using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Support.Objects.Decks;
using DeckAlchemist.Api.Sources.Deck.Mtg;
using Microsoft.AspNetCore.Mvc;
using DeckAlchemist.Api.Sources.Collection;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
    [Route("api/decks")]
    public class DecksController : Controller
    {
        readonly IMtgDeckSource _deckSource;
        readonly ICollectionSource _collectionSource;

        public DecksController(IMtgDeckSource deckSource, ICollectionSource collectionSource)
        {
            _deckSource = deckSource;
            _collectionSource = collectionSource;
        }

        // GET: api/values
        [HttpGet("all")]
        public IActionResult GetAllDecks()
        {   //IList<IMtgDeck>
            return Json(null);
        }
        [HttpGet("ID")]
        public IActionResult GetByID([FromBody] string deckId)
        {

        }

        [HttpGet("name")]
        public IActionResult GetByName([FromBody]string deckname)
        {
            var result = GetByNameInternal(deckname);
            return Json(result);
        }
        private IMtgDeck GetByNameInternal(string deckname)
        {
            return null;
        }
        private List<IMtgDeck> GetMultipleByName(List<string> deckNames)
        {
            return null;
        }
        [HttpGet("search")]
        public IActionResult Search([FromBody] List<string> decks)
        {
            var uId = Auth.UserInfo.Id(HttpContext.User);
            var userEmail = Auth.UserInfo.Email(HttpContext.User);
            var deckLists = GetMultipleByName(decks);
            var collection = _collectionSource.GetCardListFromCollection(uId);

            List<string> buildable = new List<string>();

            foreach (var deck in deckLists)
            {
                var decklist = deck.Cards;
                if (collection.Contains(decklist))
                {
                    buildable.Add(deck.id);
                }
            }
            //do some comparisons here
            return Json(buildable);
        }
    }
}
