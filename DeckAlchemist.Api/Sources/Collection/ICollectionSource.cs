using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Collection;

namespace DeckAlchemist.Api.Sources.Collection
{
    public interface ICollectionSource
    {
        bool addCardToCollection(string uId, IEnumerable<string> cardName);
        bool removeCardFromCollection(string uId, IEnumerable<string> cardName);
        bool markCardAsLent(string uId, IEnumerable<string> cardName);
        bool addCardAsLent(string uId, IEnumerable<string> cardName);
    }
}
