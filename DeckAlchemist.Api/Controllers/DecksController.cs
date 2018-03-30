using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Support.Objects.Decks;
using DeckAlchemist.Api.Sources.Deck.Mtg;
using Microsoft.AspNetCore.Mvc;
using DeckAlchemist.Api.Sources.Collection;
using Microsoft.AspNetCore.Authorization;
using DeckAlchemist.Api.Sources.Cards.Mtg;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
    [Route("api/decks")]
    public class DecksController : Controller
    {
        readonly IMtgDeckSource _deckSource;
        readonly ICollectionSource _collectionSource;
        readonly IMtgCardSource _mtgCardSource;

        public DecksController(IMtgDeckSource deckSource, ICollectionSource collectionSource, IMtgCardSource mtgCardSource)
        {
            _deckSource = deckSource;
            _collectionSource = collectionSource;
            _mtgCardSource = mtgCardSource;
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
            var result = GetByIDInternal(deckId);
            return Json(result);
        }

        [HttpGet("name")]
        public IActionResult GetByName([FromBody]string deckname)
        {
            var result = GetByNameInternal(deckname);
            return Json(result);
        }
        private IMtgDeck GetByNameInternal(string deckname)
        {
            return _deckSource.GetByName(deckname);
        }
        private List<IMtgDeck> GetMultipleByName(List<string> deckNames)
        {
            List<IMtgDeck> result = new List<IMtgDeck>();
            foreach(var deck in deckNames){
                result.Add(GetByNameInternal(deck));
            }
            return result;
        }
        private IMtgDeck GetByIDInternal(string deckId)
        {
            return _deckSource.GetById(deckId);
        }
        private List<IMtgDeck> GetMultipleByID(List<string> deckIDs)
        {
            List<IMtgDeck> result = new List<IMtgDeck>();
            foreach (var deck in deckIDs)
            {
                result.Add(GetByIDInternal(deck));
            }
            return result;
        }

        [HttpGet("search")]
        public IActionResult Search([FromBody] string typeOfSearch, List<string> decks)
        {
            if(!(typeOfSearch == "ID" || typeOfSearch == "Name")){
                return StatusCode(400);
            }

            var uId = Auth.UserInfo.Id(HttpContext.User);
            var userEmail = Auth.UserInfo.Email(HttpContext.User);
            List<IMtgDeck> deckLists;

            if (typeOfSearch == "ID")
            {
                deckLists = GetMultipleByName(decks);
            }
            else{
                deckLists = GetMultipleByID(decks);
            }

            var collection = _collectionSource.GetCollection(uId);
            Dictionary<string, int> usableCards = new Dictionary<string, int>();


            foreach( var card in collection.BorrowedCards){
                usableCards.Add(card.CardId, card.AmountBorrowed);
            }
            foreach (var card in collection.OwnedCards){
                usableCards.Add(card.CardId, card.Available);
            }

            List<string> buildable = new List<string>();
            bool status;

            foreach (var deck in deckLists)
            {
                status = true;
                var decklist = deck.Cards;
                foreach(var card in decklist){
                    if(usableCards.ContainsKey((card.Value.Name))){
                        if (usableCards[card.Value.Name] < card.Value.Count ){
                            status = false;
                            break;
                        }
                    }else{
                        status = false;
                        break;
                    }
                }

                if(status){
                    buildable.Add(deck.id);
                }
            }
            //do some comparisons here
            return Json(buildable);
        }
    }
}
