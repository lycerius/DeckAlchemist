using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Decks;

namespace DeckAlchemist.Collector.Sources.Decks.Mtg.External
{
    public interface IMtgExternalDeckSource
    {
        IEnumerable<IMtgDeck> GetAllDecks();
    }
}
