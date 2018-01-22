using System;

namespace DeckAlchemist.WebApp.Api.Objects.CardDatabase {

    public class Card : ICard{

        public bool Equals(ICard c)
        {
            return Card.IsEquals(this, c);
        }

        public string Name { get; set; }
        public string ManaCost { get; set; }
        public string ConvertedManaCost { get; set; }
        public string[] Colors { get; set; }
        public string Type { get; set; }
        public string[] Types { get; set; }
        public string[] Subtypes { get; set; }
        public string Text { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public string ImageName { get; set; }
        public Legality[] Legalities { get; set; }
        public string[] ColorIdentity { get; set; }

        public static bool IsEquals (Card c1, ICard c2) {
            if (c1 == null || c2 == null) {
                if (c1 != c2) return false;
                return true;
            }

            var equal =
                NullOrEqual (c1.ManaCost, c2.ManaCost) &&
                NullOrEqual (c1.ManaCost, c2.ManaCost) &&
                NullOrEqual (c1.Type, c2.Type) &&
                NullOrEqual (c1.Text, c2.Text) &&
                NullOrEqual (c1.Power, c2.Power) &&
                NullOrEqual (c1.Toughness, c2.Toughness) &&
                NullOrEqual (c1.ImageName, c2.ImageName) &&
                ArrayNullOrLengthEqual (c1.Colors, c2.Colors) &&
                ArrayNullOrLengthEqual (c1.Types, c2.Types) &&
                ArrayNullOrLengthEqual (c1.Subtypes, c2.Subtypes) &&
                ArrayNullOrLengthEqual (c1.Legalities, c2.Legalities) &&
                ArrayNullOrLengthEqual (c1.ColorIdentity, c2.ColorIdentity);

            if (!equal) return false;

            return CompareArrayPairs (c1.Colors, c2.Colors, c1.ColorIdentity, c2.ColorIdentity,
                c1.Legalities, c2.Legalities, c1.Subtypes, c2.Subtypes, c1.Types, c2.Types);

        }

        private static bool ArrayNullOrLengthEqual (Array a1, Array a2) {
            if (a1 == null && a2 == null) return true;

            if (a1 == null || a2 == null) return false;

            return a1.Length == a2.Length;

        }
        private static bool NullOrEqual (object o1, object o2) {
            if (o1 == null || o2 == null) {
                if (o1 == o2) return true;
                return false;
            }

            return o1.Equals (o2);
        }

        private static bool CompareArrayPairs (params Array[] arrays) {

            //Now we compare values
            bool notFinished = true;
            var i = 0;
            while (notFinished) {
                for (var f = 0; f < arrays.Length; f += 2) {
                    var array1 = arrays[f];
                    var array2 = arrays[f + 1];
                    if (array1 == null || array2 == null) {
                        if (array1 == null && array2 == null) { notFinished = false; continue; } else return false;
                    }
                    //If i is larger than the array length, then this pair has finished comparing
                    if (i >= array1.Length) { notFinished = false; continue; }

                    //Not all pairs are finished comparing
                    notFinished = true;
                    if (!array1.GetValue (i).Equals (array2.GetValue (i))) return false;
                }
                i++;
            }
            return true;
        }
    }

}