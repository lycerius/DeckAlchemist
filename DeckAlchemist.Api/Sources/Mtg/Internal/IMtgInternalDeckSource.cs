using System.Collections.Generic;
using DeckAlchemist.Collector.Objects.Decks;

namespace DeckAlchemist.Api.Sources.Mtg.Internal
{
    public interface IMtgInternalDeckSource
    {
        void UpdateAllDecks(IEnumerable<IMtgDeck> externalDecks);
    }
}
