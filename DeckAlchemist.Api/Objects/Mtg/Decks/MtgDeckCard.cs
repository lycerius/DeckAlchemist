using System;
namespace DeckAlchemist.Collector.Objects.Decks
{
    public class MtgDeckCard : IMtgDeckCard
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
