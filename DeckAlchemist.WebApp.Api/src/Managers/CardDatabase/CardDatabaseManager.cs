using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Managers.Source;
using DeckAlchemist.WebApp.Api.Objects;

namespace DeckAlchemist.WebApp.Api.Managers
{
    public class CardDatabaseManager : ICardDatabaseManager
    {
        private readonly ICardDatabaseSource _source;
        public CardDatabaseManager(ICardDatabaseSource source)
        {
            _source = source;
        }
        public IEnumerable<Card> GetAllCards()
        {
            return _source.GetAllCards();
        }
    }
}