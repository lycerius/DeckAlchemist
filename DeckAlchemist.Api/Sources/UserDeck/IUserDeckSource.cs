using System.Collections.Generic;
using DeckAlchemist.Support.Objects.UserDeck;
namespace DeckAlchemist.Api.Sources.UserDeck
{
    public interface IUserDeckSource
    {
        bool CreateDeck(string uId, string deckName);
        bool DeleteDeck(string uId, string deckName);
        bool AddCardToDeck(string uId, string deckName, string cardName);
        bool RemoveCardFromDeck(string uId, string deckName, string cardName);
        IUserDeck GetDeckByName(string uId, string deckName);
        IEnumerable<IUserDeck> GetAll(string userId);
    }

}
