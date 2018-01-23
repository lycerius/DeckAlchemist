namespace DeckAlchemist.WebApp.Api.Objects.Deck
{
    public interface IDeck
    {
        string Name {get; set;}
        CCGType CCG {get; set;}
        double Meta {get; set;}
        DeckCardCollection Cards { get; set;}

    }
}