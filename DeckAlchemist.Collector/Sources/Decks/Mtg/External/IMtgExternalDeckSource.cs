using System;
using System.Collections.Generic;
using DeckAlchemist.Collector.Objects.Decks;

namespace DeckAlchemist.Collector.Sources.Decks.Mtg.External
{
    public interface IMtgExternalDeckSource
    {
        IEnumerable<IMtgDeck> GetAllDecks();
    }
}
