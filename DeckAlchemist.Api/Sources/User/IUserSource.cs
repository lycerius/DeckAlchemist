using System;
namespace DeckAlchemist.Api.Sources.User
{
    public interface IUserSource
    {
        bool UserExists(string recivingUser);
        string GetUIDByName(string recivingUser);
    }
}
