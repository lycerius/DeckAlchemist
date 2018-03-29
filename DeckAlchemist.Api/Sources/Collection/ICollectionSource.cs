using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Cards;
using DeckAlchemist.Support.Objects.Collection;
using DeckAlchemist.Support.Objects.User;

namespace DeckAlchemist.Api.Sources.Collection
{
    public interface ICollectionSource
    {
        bool AddCardsToCollection(string uId, IList<string> cardNames);
        bool RemoveCardsFromCollection(string uId, IList<string> cardNames);
        bool MarkCardAsLent(string uId, IList<string> cardName);
        bool AddCardAsLent(string uId, IList<string> cardName);
        void Init();
        void Create(ICollection collec);
        void Update(ICollection collection);
        bool ExistsForUser(string userId);
        ICollection GetCollection(string userId);
    }
}
