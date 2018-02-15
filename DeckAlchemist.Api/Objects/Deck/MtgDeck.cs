using System;
using System.Collections.Generic;

namespace DeckAlchemist.Api.Objects.Deck
{
    public class MtgDeck : IMtgDeck
    {
        public string Name { get; set; }
        public double Meta { get; set; }
        public string id { get; set; }
        public IDictionary<string, IMtgDeckCard> Cards { get; set; }
    }
}
