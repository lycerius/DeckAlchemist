using System;

namespace DeckAlchemist.Support.Objects.User
{
    public class MongoUser : IUser
    {
        public static MongoUser FromUser(IUser user)
        {
            return new MongoUser
            {

            };
        }
    }
}
