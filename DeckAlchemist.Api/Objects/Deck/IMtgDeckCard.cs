using System;
namespace DeckAlchemist.Api.Objects.Deck
{
    public interface IMtgDeckCard
    {
        string Name { get; set; }
        int Count { get; set; }
    }
}
