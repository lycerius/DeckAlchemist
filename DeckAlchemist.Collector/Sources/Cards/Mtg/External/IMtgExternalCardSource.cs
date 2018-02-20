using System.Collections.Generic;
using DeckAlchemist.Api.Objects.Card.Mtg;

namespace DeckAlchemist.Collector.Sources.Cards.Mtg
{
    public interface IMtgExternalCardSource
    {
        IEnumerable<IMtgCard> GetAllCards();
    }
}
