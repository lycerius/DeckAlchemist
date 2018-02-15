using System;
using System.Collections.Generic;
using DeckAlchemist.Collector.Objects.Decks;

namespace DeckAlchemist.Collector.Sources.Decks.Mtg.Internal
{
    public interface IMtgInternalDeckSource
    {
        void UpdateAllDecks(IEnumerable<IMtgDeck> externalDecks);
    }
}
