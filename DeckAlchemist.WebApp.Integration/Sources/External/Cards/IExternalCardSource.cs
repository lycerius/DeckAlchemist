namespace DeckAlchemist.WebApp.Integration.Sources.External.Cards
{
    public interface IExternalCardSource
    {
         CardDatabaseCollection GetAllCards ();
    }
}