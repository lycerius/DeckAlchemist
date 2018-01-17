using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Managers.Caching;
using DeckAlchemist.WebApp.Api.Managers.Source;
using DeckAlchemist.WebApp.Api.Objects;

namespace DeckAlchemist.WebApp.Api.Managers
{
    public class CachingCardDatabaseManager : ICardDatabaseManager
    {
        private readonly ICardDatabaseSource _source;
        private readonly ICardDatabaseCache _cache;

        public CachingCardDatabaseManager(ICardDatabaseSource source, ICardDatabaseCache cache)
        {
            _source = source;
            _cache = cache;
        }

        public IEnumerable<Card> GetAllCards()
        {
            if(_cache.IsInvalid())
            {
                _cache.Set(_source.GetAllCards());
            }

            return _cache.Get();
        }
    }
}