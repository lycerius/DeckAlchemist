using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Objects;

namespace DeckAlchemist.WebApp.Api.Managers.Caching
{
    public class InMemoryCardDatabaseCache : ICardDatabaseCache
    {
        private IEnumerable<Card> _cache;
        private bool _invalid = true;

        public IEnumerable<Card> Get()
        {
            if(_invalid) return null;
            lock(_cache) {
                return _cache;
            }
        }

        public void Invalidate()
        {
            _invalid = true;
        }

        public bool IsInvalid()
        {
            return _invalid;
        }

        public void Set(IEnumerable<Card> cache)
        {
            lock(_cache)
            {
                _cache = cache;
                _invalid = false;
            }
        }
    }
}