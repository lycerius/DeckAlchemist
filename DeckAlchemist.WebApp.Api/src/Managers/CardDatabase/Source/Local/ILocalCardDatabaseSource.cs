using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Objects.CardDatabase;

namespace DeckAlchemist.WebApp.Api.Managers.CardDatabase.Source.Local {
    public interface ILocalCardDatabaseSource {
        CardDatabaseCollection GetAllCards ();

        CardDatabaseCollection GetAllStandardCards();

        void Update (params ICard[] cards);
    }
}