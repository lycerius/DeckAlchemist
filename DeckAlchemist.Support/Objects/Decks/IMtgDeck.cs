using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Decks
{
    public interface IMtgDeck
    {
        string Name { get; set; }
        double Meta { get; set; }
        string id { get; set; }
        IDictionary<string, IMtgDeckCard> Cards { get; set; }
    }
}