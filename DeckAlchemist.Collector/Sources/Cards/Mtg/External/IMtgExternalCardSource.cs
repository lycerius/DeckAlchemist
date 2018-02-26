using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Cards;

namespace DeckAlchemist.Collector.Sources.Cards.Mtg
{
    public interface IMtgExternalCardSource
    {
        IEnumerable<IMtgCard> GetAllCards();
    }
}
