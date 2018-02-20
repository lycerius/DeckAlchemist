using System.Collections.Generic;
using DeckAlchemist.Api.Objects.Card.Mtg;

namespace DeckAlchemist.Api.Sources.Cards.Mtg
{
    public interface IMtgCardSource
    {
        IEnumerable<IMtgCard> GetCardsByNames(params string[] names);
        
        void UpdateAllCards(IEnumerable<IMtgCard> cards);
    }
}
