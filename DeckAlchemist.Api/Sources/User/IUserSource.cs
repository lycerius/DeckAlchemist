using System;
using DeckAlchemist.Support.Objects.User;

namespace DeckAlchemist.Api.Sources.User
{
    public interface IUserSource
    {
<<<<<<< HEAD
        void Initialize():
        IUser Get(string userId);
        void Update(IUser user);
        void Create(IUser user);
=======
        bool UserExists(string recivingUser);
        string GetUIDByName(string recivingUser);
>>>>>>> 853fd09c8ff411564306df816fab2c75416da9ce
    }
}
