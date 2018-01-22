using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Objects.CardDatabase;
using DeckAlchemist.WebApp.Api.Managers.CardDatabase;

namespace DeckAlchemist.WebApp.Api.Managers.CardDatabase.Source.External {
    public interface IExternalCardDatabaseSource {
        CardDatabaseCollection GetAllCards ();
    }
}