using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Objects.CardDatabase;

namespace DeckAlchemist.WebApp.Api.Managers.CardDatabase.Source.Local {
    public interface ILocalCardDatabaseSource {
        CardDatabaseCollection GetAllCards ();

        void Update (params ICard[] cards);
    }
}