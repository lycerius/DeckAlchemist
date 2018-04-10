using System.Collections.Generic;
using DeckAlchemist.Support.Objects.User;
using DeckAlchemist.Api.Sources.Group;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DeckAlchemist.Support.Objects.Group;
using DeckAlchemist.Api.Utility;
using System;
using DeckAlchemist.Api.Sources.User;
using DeckAlchemist.Api.Contracts;
using System.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
    [Route("api/group")]
    public class GroupsController : Controller
    {

        readonly IGroupSource _source;
        readonly IUserSource _userSource;

        public GroupsController(IGroupSource source, IUserSource userSource)
        {
            _source = source;
            _userSource = userSource;
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

        [HttpGet("{groupId}")]
        public IActionResult GetGroupInformation(string groupId)
        {
            try
            {
                return Json(_source.GetGroupInfo(groupId));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("all")]
        public IActionResult GetAllUserGroups()
        {
            var userId = HttpContext.User.Id();

            var user = _userSource.Get(userId);
            if (user == null) return null;

            var memberOf = user.Groups;

            var groups = _source.GetGroups(memberOf);
            return Json(groups.Select(group => {
                var memberNames = _userSource.GetUserNamesByUserIds(group.Members.ToArray());

                return new GroupModel
                {
                    GroupId = group.GroupId,
                    GroupName = group.GroupName,
                    Users = group.Members.Select(memberID =>
                    {
                        return new UserModel
                        {
                            UserId = memberID,
                            UserName = memberNames[memberID]
                        };
                    })
                };
            }));
        }


        [HttpPost("{groupName}/create")]
        public IActionResult CreateGroup(string groupName)
        {
            try
            {
                var userId = HttpContext.User.Id();
                var user = _userSource.Get(userId);


                var group = new Group
                {
                    GroupName = groupName,
                    Members = new List<string>() {userId},
                    Owner = HttpContext.User.Id(),
                    GroupId = Guid.NewGuid().ToString()
                };

                _source.CreateGroup(group);
                user.Groups.Add(group.GroupId);
                _userSource.Update(user);

                return Ok();
            }catch
            {
                return StatusCode(500);
            }
            
        }

    }
}
