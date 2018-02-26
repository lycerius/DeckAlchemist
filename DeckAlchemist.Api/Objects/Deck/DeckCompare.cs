using System;
using System.Linq;
using System.Numerics;

namespace DeckAlchemist.Api.Objects.Deck
{
    public static class DeckCompare
    {
        public static Vector2[] FeatureSpaceFor(IMtgDeck deck)
        {
            Vector2[] space = new Vector2[deck.Cards.Count];

            int i = 0;
            foreach (var key in deck.Cards.Keys)
            {
                var card = deck.Cards[key];
                
                Vector2 features = new Vector2(card.FeatureIndex, card.Count);

                space[i] = features;
                i++;
            }

            return space;
        }

        public static float VectorCompare(Vector2[] deck1Space, Vector2[] deck2Space)
        {
            deck1Space = deck1Space.OrderByDescending(v => v.X).ToArray();
            deck2Space = deck2Space.OrderByDescending(v => v.X).ToArray();
            
            int smallerSize = Math.Min(deck1Space.Length, deck2Space.Length);

            float dSum = 0f;
            for (int i = 0; i < smallerSize; i++)
            {
                var f1 = deck1Space[i];
                var f2 = deck2Space[i];

                dSum += Vector2.DistanceSquared(f1, f2);
            }

            float featureScore = dSum / smallerSize;

            return featureScore;
        }

        public static float Compare(IMtgDeck deck1, IMtgDeck deck2)
        {
            Vector2[] deck1Space = FeatureSpaceFor(deck1);
            Vector2[] deck2Space = FeatureSpaceFor(deck2);

            return VectorCompare(deck1Space, deck2Space);
        }
        
        
    }
}