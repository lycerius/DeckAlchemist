using System.Collections.Generic;
using DeckAlchemist.Api.Objects.Deck;

namespace DeckAlchemist.Collector.Sources.Decks.Mtg.External
{
    public interface IMtgExternalDeckSource
    {
        IEnumerable<IMtgDeck> GetAllDecks();
    }
}
