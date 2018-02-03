using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Managers;
using DeckAlchemist.WebApp.Api.Managers.Deck;
using DeckAlchemist.WebApp.Api.Objects.CardDatabase;
using DeckAlchemist.WebApp.Api.Objects.Deck;
using Microsoft.AspNetCore.Mvc;

namespace DeckAlchemist.WebApp.Api.Controllers
{
    [Route("api/v1/decks")]
    public class DeckController : Controller
    {
        private readonly IDeckManager _deckManager;

        public DeckController(IDeckManager deckManager)
        {
            _deckManager = deckManager;
        }

        [HttpGet("all")]
        public IEnumerable<IDeck> GetAllDecks()
        {
            return _deckManager.GetAllDecks().Collection;
        }

        [HttpGet("all/extended")]
        public IEnumerable<IDeck> GetAllDecksExtended()
        {
            return _deckManager.GetAllDecksExtended().Collection;
        }
    }
}