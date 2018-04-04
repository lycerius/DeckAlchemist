using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Collection;

namespace DeckAlchemist.Api.Contracts
{
    public class OwnedCardsModel
    {
        public IDictionary<string, Support.Objects.Cards.IMtgCard> CardInfo;
    }
}
