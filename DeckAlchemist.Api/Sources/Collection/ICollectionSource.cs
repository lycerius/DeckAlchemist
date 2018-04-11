using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Collection;

namespace DeckAlchemist.Api.Sources.Collection
{
    public interface ICollectionSource
    {
        bool AddCardToCollection(string uId, IEnumerable<string> cardName);
        bool AddCardToCollection(string uId, IDictionary<string, int> cardsAndAmounts);
        bool RemoveCardFromCollection(string uId, IEnumerable<string> cardName);
        bool MarkCardAsLent(string lender, string lendee, IDictionary<string, int> namesAndAmounts);
        bool AddCardAsLent(string lender, string lendee, IDictionary<string, int> namesAndAmounts);
        bool MarkCardsAsLendable(string userId, IDictionary<string, bool> lendingStatus);
        void Init();
        void Create(ICollection collec);
        void Update(ICollection collection);
        bool ExistsForUser(string userId);
        ICollection GetCollection(string userId);
    }
}
