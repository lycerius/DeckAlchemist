using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Cards;
using DeckAlchemist.Support.Objects.Collection;

namespace DeckAlchemist.Api.Contracts
{
    public class OwnedCardsModel
    {
        public IDictionary<string, IOwnedCard> OwnedCards { get; set; }
        public IDictionary<string, IMtgCard> CardInfo { get; set; }
    }
}
