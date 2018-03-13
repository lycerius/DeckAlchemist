using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DeckAlchemist.Api.Auth;
using DeckAlchemist.Api.Sources.Collection;
using DeckAlchemist.Api.Sources.User;
using DeckAlchemist.Support.Objects.Collection;
using DeckAlchemist.Support.Objects.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
    [Route("api/login")]
    public class LoginController : Controller
    {
        ICollectionSource _collectionSource;
        IUserSource _userSource;

        public LoginController(ICollectionSource collectionSource, IUserSource userSource)
        {
            _collectionSource = collectionSource;
            _userSource = userSource;
        }
        [HttpGet]
        public string Login()
        {
            var userInfo = HttpContext.User;

            //Check to see if the user is created (first time login)
            CreateUserIfNotExist(userInfo);


            //Check to see if the user's collection is create
            CreateCollectionIfNotExist(userInfo);
            return null;
        }

        void CreateUserIfNotExist(ClaimsPrincipal user)
        {
            var userId = UserInfo.Id(user);
            if (!_userSource.UserExists(userId))
            {
                var email = UserInfo.Email(user);
                var newUser = new User
                {
                    UserId = userId,
                    Email = email,
                    UserName = email
                };
                _userSource.Create(newUser);
            }
        }

        void CreateCollectionIfNotExist(ClaimsPrincipal userInfo)
        {
            var userId = UserInfo.Id(userInfo);
            if(!_collectionSource.ExistsForUser(userId))
            {
                var user = _userSource.Get(userId);
                var collection = new Collection
                {
                    UserId = userId,
                    CollectionId = Guid.NewGuid().ToString()
                };
                _collectionSource.Create(collection);
                user.CollectionId = collection.CollectionId;
                _userSource.Update(user);
            }
        }

    }
}
