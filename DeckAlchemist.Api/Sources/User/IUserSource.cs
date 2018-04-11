using System.Collections.Generic;
using DeckAlchemist.Support.Objects.User;

namespace DeckAlchemist.Api.Sources.User
{
    public interface IUserSource
    {
        void Init();
        IUser Get(string userId);
        IUser GetUserByUserName(string userName);
        void Update(IUser user);
        void Create(IUser user);
        bool UserExists(string recivingUser);
        IDictionary<string, string> GetUserNamesByUserIds(string[] userIds);
    }
}
