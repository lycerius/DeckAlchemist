using System.Collections.Generic;

namespace DeckAlchemist.WebApp.Api.Objects.Deck
{
    public interface ICardDeckCollection
    {
        IDictionary<string, IDeckCard> Collection {get;}
    }
}