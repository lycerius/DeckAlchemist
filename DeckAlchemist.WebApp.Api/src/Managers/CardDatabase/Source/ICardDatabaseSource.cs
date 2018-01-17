
using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Objects;

namespace DeckAlchemist.WebApp.Api.Managers.Source
{
    public interface ICardDatabaseSource
    {
        IEnumerable<Card> GetAllCards();
    }
}
