using System;
namespace DeckAlchemist.Api.Objects.Deck
{
    public class MtgDeckCard : IMtgDeckCard
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
