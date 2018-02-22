using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Api.Objects.Group;
using DeckAlchemist.Api.Objects.User;
using DeckAlchemist.Api.Sources.Group;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Route("api/group")]
    public class GroupsController : Controller
    {

        readonly IGroupSource _source;

        public GroupsController(IGroupSource source)
        {
            _source = source;
        }

        // GET api/group/{name}/all
        [HttpGet("{name}/all")]
        public IEnumerable<IUser> Get(string name)
        {
            var members = _source.GetAllUsers(name);
            return members;
        }

        // PUT api/group/{name}/member
        [HttpPut("{groupName}/member")]
        public IActionResult AddMember(string groupName, [FromBody]string userName)
        {
            try
            {
                _source.AddUser(groupName, userName);
                return Ok();
            }
            catch{
                return StatusCode(500);
            }

        }

        // PUT api/group/{name}/member
        [HttpDelete("{groupName}/member")]
        public IActionResult RemoveMember(string groupName, [FromBody]string userName){
            try
            {
                _source.RemoveUser(groupName, userName);
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
