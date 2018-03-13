using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Collection;
using DeckAlchemist.Support.Objects.User;

namespace DeckAlchemist.Api.Sources.Collection
{
    public interface ICollectionSource
    {
<<<<<<< HEAD
        bool AddCardToCollection(string uId, IList<string> cardName);
        bool RemoveCardFromCollection(string uId, IList<string> cardName);
        bool MarkCardAsLent(string uId, IList<string> cardName);
        bool AddCardAsLent(string uId, IList<string> cardName);
=======
        void Init();
        string Create(ICollection collec);
        void Update(ICollection collection);
        bool AddCardToCollection(string uId, IEnumerable<string> cardName);
        bool RemoveCardFromCollection(string uId, IEnumerable<string> cardName);
        bool MarkCardAsLent(string uId, IEnumerable<string> cardName);
        bool AddCardAsLent(string uId, IEnumerable<string> cardName);
        bool ExistsForUser(string userId);
>>>>>>> 72020bae85c7c42372a692fb430fc49a77091d08
    }
}
