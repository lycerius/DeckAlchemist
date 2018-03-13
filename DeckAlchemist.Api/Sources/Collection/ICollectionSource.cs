using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Collection;

namespace DeckAlchemist.Api.Sources.Collection
{
    public interface ICollectionSource
    {
        bool AddCardToCollection(string uId, IList<string> cardName);
        bool RemoveCardFromCollection(string uId, IList<string> cardName);
        bool MarkCardAsLent(string uId, IList<string> cardName);
        bool AddCardAsLent(string uId, IList<string> cardName);
    }
}
