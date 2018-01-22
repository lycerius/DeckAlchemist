using System.Collections.Generic;

namespace DeckAlchemist.WebApp.Api.Objects.Deck
{
    public class CardDeckCollection : ICardDeckCollection
    {
        public IDictionary<string, IDeckCard> Collection {get;set;}
    }
}