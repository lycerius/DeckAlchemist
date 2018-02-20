using System;
using System.Collections.Generic;
using DeckAlchemist.Api.Objects.Deck;

namespace DeckAlchemist.Api.Sources.Deck.Mtg
{
    public interface IMtgDeckSource
    {
        void UpdateAllDecks(IEnumerable<IMtgDeck> decks);
    }
}
