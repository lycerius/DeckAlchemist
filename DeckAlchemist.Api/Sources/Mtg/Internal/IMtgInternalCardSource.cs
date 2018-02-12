using System.Collections.Generic;
using DeckAlchemist.Collector.Objects.Cards;

namespace DeckAlchemist.Api.Sources.Mtg.Internal
{
    public interface IMtgInternalCardSource
    {
        IEnumerable<IMtgCard> GetAllCards();
        void UpdateAllCards(IEnumerable<IMtgCard> cards);
    }
}
