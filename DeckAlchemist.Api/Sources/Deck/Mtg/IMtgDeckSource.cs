using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Decks;

namespace DeckAlchemist.Api.Sources.Deck.Mtg
{
    public interface IMtgDeckSource
    {
        IEnumerable<IMtgDeck> GetAllDecks();
        IMtgDeck GetDeckByName(string name);
        IEnumerable<IMtgDeck> SearchDecks(string name);
    }
}
