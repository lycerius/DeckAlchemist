using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Cards;

namespace DeckAlchemist.Api.Sources.Cards.Mtg
{
    public interface IMtgCardSource
    {
        IEnumerable<IMtgCard> GetCardsByNames(params string[] names);
        bool CheckExistance(IEnumerable<string> cardNames);
    }
}
