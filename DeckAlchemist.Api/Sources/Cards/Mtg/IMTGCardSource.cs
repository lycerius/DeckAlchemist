using System;
using System.Collections.Generic;
using DeckAlchemist.Api.Objects.Cards.Mtg;

namespace DeckAlchemist.Api.Sources.Cards.Mtg
{
    public interface IMTGCardSource
    {
        IEnumerable<IMtgCard> GetCardsByNames(params string[] names);
    }
}
