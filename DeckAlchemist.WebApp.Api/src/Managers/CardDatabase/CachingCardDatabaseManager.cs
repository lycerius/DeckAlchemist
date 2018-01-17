using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Managers.Source;
using DeckAlchemist.WebApp.Api.Objects;

namespace DeckAlchemist.WebApp.Api.Managers 
{
    public class CachingCardDatabaseManager : ICardDatabaseManager
    {
        private readonly ICardDatabaseSource _source;
        private volatile IEnumerable<Card> _cache = new List<Card>();

        public CachingCardDatabaseManager(ICardDatabaseSource source){
            _source = source;
            Invalidate();
        }

        public void Invalidate() 
        {
            lock(_cache){
                _cache = _source.GetAllCards();
            }
        }

        private bool IsInvalidCache()
        {
            return _cache != null;
        }

        public IEnumerable<Card> GetAllCards()
        {
            lock(_cache) {
                return _cache;
            }
        }
    }
}