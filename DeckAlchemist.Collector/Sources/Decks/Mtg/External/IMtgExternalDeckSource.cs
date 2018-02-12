using System;
using System.Collections.Generic;
using DeckAlchemist.Api.Objects.Mtg.Decks;

namespace DeckAlchemist.Collector.Sources.Decks.Mtg.External
{
    public interface IMtgExternalDeckSource
    {
        IEnumerable<IMtgDeck> GetAllDecks();
    }
}
