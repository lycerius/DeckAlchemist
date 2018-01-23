using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Objects.CardDatabase;
using System.Linq;

namespace DeckAlchemist.WebApp.Api.Managers.CardDatabase.Source.Local {
    public class InMemoryLocalCardDatabaseSource : ILocalCardDatabaseSource {
        private volatile Dictionary<string, ICard> _storage = new Dictionary<string, ICard>();
        public CardDatabaseCollection GetAllCards () {
            lock (_storage) {
                return new CardDatabaseCollection(_storage);
            }
        }

        public void Update (params ICard[] cards) {
            lock (_storage) {
                foreach (var card in cards) {
                    _storage[card.Name] = card;
                }
            }
        }
    }
}