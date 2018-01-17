using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Managers;
using DeckAlchemist.WebApp.Api.Objects;
using Microsoft.AspNetCore.Mvc;

namespace DeckAlchemist.WebApp.Api.Controllers
{
    [Route("api/v1/cards")]
    public class CardDatabaseController : Controller 
    {
        private readonly ICardDatabaseManager _cardDatabaseManager;

        public CardDatabaseController(ICardDatabaseManager cardDatabaseManager)
        {
            _cardDatabaseManager = cardDatabaseManager;
        }

        [HttpGet("all")]
        public IEnumerable<Card> GetAllCards() {
            return _cardDatabaseManager.GetAllCards();
        }
    }
}