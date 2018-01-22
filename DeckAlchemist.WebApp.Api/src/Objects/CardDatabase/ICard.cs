namespace DeckAlchemist.WebApp.Api.Objects.CardDatabase
{
    public interface ICard
    {
        string Name { get; set; }
        string ManaCost { get; set; }
        string ConvertedManaCost { get; set; }
        string[] Colors { get; set; }
        string Type { get; set; }
        string[] Types { get; set; }
        string[] Subtypes { get; set; }
        string Text { get; set; }
        string Power { get; set; }
        string Toughness { get; set; }
        string ImageName { get; set; }
        Legality[] Legalities { get; set; }
        string[] ColorIdentity { get; set; }

        bool Equals(ICard c);
    }
}