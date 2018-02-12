using System;
using System.Collections.Generic;
using DeckAlchemist.Api.Objects.Mtg.Cards;

namespace DeckAlchemist.Collector.Sources.Cards.Mtg
{
    public interface IMtgExternalCardSource
    {
        IEnumerable<IMtgCard> GetAllCards();
    }
}
