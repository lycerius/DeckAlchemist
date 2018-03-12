using System;
namespace DeckAlchemist.Api.Sources.User
{
    public interface IUserSource
    {
        bool userExists(string recivingUser);
        string getUIDByName(string recivingUser);
    }
}
