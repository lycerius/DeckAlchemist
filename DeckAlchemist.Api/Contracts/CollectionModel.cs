using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Cards;
using DeckAlchemist.Support.Objects.Collection;

namespace DeckAlchemist.Api.Contracts
{
    public class CollectionModel
    {
        public ICollection UserCollection;
        public IDictionary<string, IMtgCard> CardInfo;
    }
}
