using System;

namespace DeckAlchemist.Support.Objects.Collection
{
    public class MongoCollection : ICollection
    {
        public static MongoCollection FromCollection(ICollection collec)
        {
            return new MongoCollection
            {

            };
        }
    }
}
