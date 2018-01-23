namespace DeckAlchemist.WebApp.Api.Objects.CardDatabase
{
    public interface ICard
    {
        string Name {get; set;}

        CCGType CCG {get; set;}

        string Image {get;set;}
    }
}