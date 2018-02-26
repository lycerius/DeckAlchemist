using System;
using System.Collections.Generic;
using DeckAlchemist.Collector.Objects.Cards;

namespace DeckAlchemist.Collector.Sources.Cards.Mtg
{
    public interface IMtgExternalCardSource
    {
        IEnumerable<IMtgCard> GetAllCards();
    }
}
