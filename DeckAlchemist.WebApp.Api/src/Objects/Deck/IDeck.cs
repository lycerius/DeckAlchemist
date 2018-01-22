namespace DeckAlchemist.WebApp.Api.Objects.Deck
{
    public interface IDeck
    {
        string Name {get;set;}
        int Meta {get;set;}
        
        ICardDeckCollection Cards{get;}
    }
}