namespace DeckAlchemist.WebApp.Api.Objects 
{

    public class Card {
        public string Name {get;set;}
        public string ManaCost {get;set;}
        public int ConvertedManaCost {get;set;}
        public string[] Colors{get;set;}
        public string Type{get;set;}
        public string[] Types{get;set;}
        public string[] Subtypes{get;set;}
        public string Text{get;set;}
        public int Power{get;set}
        public int Toughness {get;set;}
        public string ImageName{get;set;}
        public Legality[] Legalities{get;set;}
        public string[] ColorIdentity {get;set;}
    }

}