using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Api.Objects.Deck;
using DeckAlchemist.Api.Sources.Deck.Mtg;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Route("api/decks")]
    public class DecksController : Controller
    {
        readonly IMtgDeckSource _source;

        public DecksController(IMtgDeckSource source)
        {
            _source = source;
        }

        // GET: api/values
        [HttpGet("all")]
        public IEnumerable<IMtgDeck> GetAllDecks()
        {
            return _source.GetAllDecks();
        }

        [HttpGet()]
        public IMtgDeck GetByName([FromBody]string deckname)
        {
            return _source.GetDeckOfName(deckname);
        }

    }
}
