using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Objects;

namespace DeckAlchemist.WebApp.Api.Managers.Caching
{
    public interface ICardDatabaseCache 
    {
        IEnumerable<Card> Get();

        void Set(IEnumerable<Card> cache);

        bool IsInvalid();

        void Invalidate();
    }
}