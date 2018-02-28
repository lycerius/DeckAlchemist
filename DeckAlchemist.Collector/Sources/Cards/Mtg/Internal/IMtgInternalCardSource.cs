using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Cards;

namespace DeckAlchemist.Collector.Sources.Cards.Mtg.Internal
{
    public interface IMtgInternalCardSource
    {
        IEnumerable<IMtgCard> GetAllCards();
        void UpdateAllCards(IEnumerable<IMtgCard> cards);
    }
}
