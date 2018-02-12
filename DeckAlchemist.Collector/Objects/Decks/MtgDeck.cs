using System;
using System.Collections.Generic;

namespace DeckAlchemist.Collector.Objects.Decks
{
    public class MtgDeck : IMtgDeck
    {
        public string Name { get; set; }
        public double Meta { get; set; }
        public string id { get; set; }
        public IDictionary<string, IMtgDeckCard> Cards { get; set; }
    }
}
