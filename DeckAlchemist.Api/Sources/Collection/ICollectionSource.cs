using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Collection;
using DeckAlchemist.Support.Objects.User;

namespace DeckAlchemist.Api.Sources.Collection
{
    public interface ICollectionSource
    {
        bool AddCardToCollection(string uId, IList<string> cardName);
        bool RemoveCardFromCollection(string uId, IList<string> cardName);
        bool MarkCardAsLent(string uId, IList<string> cardName);
        bool AddCardAsLent(string uId, IList<string> cardName);
        void Init();
        string Create(ICollection collec);
        void Update(ICollection collection);
        bool ExistsForUser(string userId);
        ICollection GetCollection(string userId);
    }
}
