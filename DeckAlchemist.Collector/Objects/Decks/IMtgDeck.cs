using System;
using System.Collections.Generic;

namespace DeckAlchemist.Collector.Objects.Decks
{
    public interface IMtgDeck
    {
        string Name { get; set; }
        double Meta { get; set; }
        string id { get; set; }
        IDictionary<string, IMtgDeckCard> Cards { get; set; }
        float CompareDecks(IMtgDeck other);
    }
}