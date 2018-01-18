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

        public CardDatabaseCollection GetAllStandardCards()
        {
            var standardLegal = new Legality{
                Format = "Standard",
                Status = "Legal"
            };

            lock(_storage)
            {
                return new CardDatabaseCollection(_storage.Where(entry => entry.Value.Legalities != null && entry.Value.Legalities.Contains(standardLegal)).Select(item => item.Value).ToDictionary(item => item.Name));
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