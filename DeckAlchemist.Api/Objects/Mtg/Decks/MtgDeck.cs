using System;
using System.Collections.Generic;
using DeckAlchemist.Api.Objects.Mtg.Decks;

namespace DeckAlchemist.Collector.Objects.Decks
{
    public class MtgDeck : IMtgDeck
    {
        public string Name { get; set; }
        public double Meta { get; set; }
        public string id { get; set; }
        public IDictionary<string, IMtgDeckCard> Cards { get; set; }

        public static MtgDeck FromMongo(MongoMtgDeck deck)
        {
            return new MtgDeck()
            {
                Name = deck.Name,
                Meta = deck.Meta,
                Cards = deck.Cards
            };
        }
    }
}
