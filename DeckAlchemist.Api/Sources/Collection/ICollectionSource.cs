using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Collection;

namespace DeckAlchemist.Api.Sources.Collection
{
    public interface ICollectionSource
    {
        bool AddCardToCollection(string uId, IEnumerable<string> cardName);
        bool RemoveCardFromCollection(string uId, IEnumerable<string> cardName);
        bool MarkCardAsLent(string uId, IEnumerable<string> cardName);
        bool AddCardAsLent(string uId, IEnumerable<string> cardName);
    }
}
