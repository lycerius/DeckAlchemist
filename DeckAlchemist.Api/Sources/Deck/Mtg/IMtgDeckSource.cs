using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Decks;

namespace DeckAlchemist.Api.Sources.Deck.Mtg
{
    public interface IMtgDeckSource
    {
        IMtgDeck GetById(string deckID);
        IMtgDeck GetByName(string deckName);
    }
}
