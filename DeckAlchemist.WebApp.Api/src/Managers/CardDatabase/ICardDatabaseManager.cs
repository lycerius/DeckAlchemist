using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Objects;

namespace DeckAlchemist.WebApp.Api.Managers
{
    public interface ICardDatabaseManager
    {
        IEnumerable<Card> GetAllCards();
    }
}