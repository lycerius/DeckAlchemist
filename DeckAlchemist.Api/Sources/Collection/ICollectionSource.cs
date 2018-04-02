using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Cards;
using DeckAlchemist.Support.Objects.Collection;
using DeckAlchemist.Support.Objects.User;

namespace DeckAlchemist.Api.Sources.Collection
{
    public interface ICollectionSource
    {
        bool AddCardToCollection(string uId, IEnumerable<string> cardName);
        bool RemoveCardFromCollection(string uId, IEnumerable<string> cardName);
        bool MarkCardAsLent(string lender, string lendee, IDictionary<string, int> namesAndAmounts);
        bool AddCardAsLent(string lender, string lendee, IDictionary<string, int> namesAndAmounts);
        void Init();
        void Create(ICollection collec);
        void Update(ICollection collection);
        bool ExistsForUser(string userId);
        ICollection GetCollection(string userId);
    }
}
