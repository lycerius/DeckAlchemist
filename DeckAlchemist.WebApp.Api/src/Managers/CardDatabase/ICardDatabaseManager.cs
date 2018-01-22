using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Managers.CardDatabase;
using DeckAlchemist.WebApp.Api.Objects.CardDatabase;

namespace DeckAlchemist.WebApp.Api.Managers {
    public interface ICardDatabaseManager {
        CardDatabaseCollection GetAllCards ();

        CardDatabaseCollection GetAllStandard ();
    }
}