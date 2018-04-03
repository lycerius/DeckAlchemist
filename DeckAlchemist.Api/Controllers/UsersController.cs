using DeckAlchemist.Api.Sources.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DeckAlchemist.Api.Utility;
using DeckAlchemist.Support.Objects.User;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
    [Route("api/user")]
    public class UsersController : Controller
    {

        readonly IUserSource _source;

        public UsersController(IUserSource source)
        {
            _source = source;
        }

        [HttpGet]
        public IUser GetUserInfo()
        {
            var uId = HttpContext.User.Id();
            return _source.Get(uId);
        }
    }
}
