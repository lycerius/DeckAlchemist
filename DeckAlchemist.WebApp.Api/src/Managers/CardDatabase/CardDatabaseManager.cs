using System.Collections.Generic;
using System.Linq;
using DeckAlchemist.WebApp.Api.Managers.CardDatabase;
using DeckAlchemist.WebApp.Api.Managers.CardDatabase.Source.Local;
using DeckAlchemist.WebApp.Api.Objects.CardDatabase;

namespace DeckAlchemist.WebApp.Api.Managers {
    public class CardDatabaseManager : ICardDatabaseManager {
        private readonly ILocalCardDatabaseSource _source;

        public CardDatabaseManager (ILocalCardDatabaseSource source) {
            _source = source;
        }
        public CardDatabaseCollection GetAllCards () {
            return _source.GetAllCards ();
        }

    }
}