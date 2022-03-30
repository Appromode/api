using log4net;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using marking_api.DataModel.Project;
using Microsoft.AspNetCore.Mvc;
using marking_api.DataModel.API;
using marking_api.Global.Extensions;
using marking_api.DataModel.DTOs;
using marking_api.DataModel.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using marking_api.API.Models.Identity;
using System.Collections.Generic;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork, ILog logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserDTO)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Users.Get());
        }

        [HttpGet("{id}/Users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserDTO)))]
        public IActionResult GetUsers(string id)
        {
            var users = _unitOfWork.Users.Get(filter: (table) => table.Id != id);

            return Ok(users);
        }

        [HttpGet("{id}/Invites")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserDTO)))]
        public IActionResult GetInvites(string id)
        {
            var cm = new UserCM(_unitOfWork, _logger);

            if (cm != null) {
                return Ok(cm.GetInvites(id));
            }
            return NoContent();
        }

        [HttpPost("{inviteId}/Invite/Accept")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(InviteDM)))]
        public IActionResult AcceptInvite([FromBody] Invite invite)
        {
            var cm = new UserCM(_unitOfWork, _logger);

            var group = cm.AcceptInvite(invite.InviteId);

            var acceptedInvite = _unitOfWork.Invites.GetById(invite.InviteId);

            return Ok(acceptedInvite);
        }

        [HttpPost("{inviteId}/Invite/Reject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(InviteDM)))]
        public IActionResult RejectInvite([FromBody] Invite invite)
        {
            var cm = new UserCM(_unitOfWork, _logger);

            var rejectedInvite = cm.RejectInvite(invite.InviteId);

            return Ok(rejectedInvite);
        }
        
        [HttpGet("{id}/AvailableTags")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(TagDM)))]
        public IActionResult GetAvailableTags(string id)
        {
            User user = _unitOfWork.Users.GetById(id);

            if (user == null) {
                NoContent();
            }

            var userTags = _unitOfWork.UserTags.Get(
                include: (userTags) => userTags.Include((userTags) => userTags.Tag),
                filter: (userTags) => userTags.UserId == id
                ).ToList();

            var tags = userTags.ToList().Select((userTag) => userTag.Tag).ToList();

            var results = _unitOfWork.Tags.Get(
                filter: (tag) => !tags.Contains(tag)
            ).ToList();

            if (results.Count() <= 0) {
                return NoContent();
            }

            return Ok(results);
        }

        [HttpGet("{id}/Tags")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(TagDM)))]
        public IActionResult GetTags(string id)
        {
            User user = _unitOfWork.Users.GetById(id);

            if (user == null) {
                NoContent();
            }

            var userTags = _unitOfWork.UserTags.Get(
                include: (userTags) => userTags.Include((userTags) => userTags.Tag),
                filter: (userTags) => userTags.UserId == id
                )
                .Select((table) => table.Tag)
                .ToList();

            if (userTags.Count() <= 0) {
                return NoContent();
            }

            return Ok(userTags);
        }

        [HttpDelete("{tagId}/Tag")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserDTO)))]
        public IActionResult Delete(RemoveTagRequest removeTagRequest)
        {
            var user = _unitOfWork.Users.GetById(removeTagRequest.UserId);

            if (user == null)
                return NotFound();

            var rowsToDelete = _unitOfWork.UserTags.Get(
                filter: (table) => (
                    table.TagId == removeTagRequest.TagId &&
                    table.UserId == removeTagRequest.UserId
                )
            );

            _unitOfWork.UserTags.DeleteRange(rowsToDelete);

            _unitOfWork.Save();

            return Ok(rowsToDelete);
        }

        [HttpPost("Tags")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(TagDM)))]
        public IActionResult AddTags([FromBody] AddTagRequest tagRequest)
        {
            var strategy = _unitOfWork.GenericMethods.CreateExecutionStrategy();

            if (tagRequest.UserId == null)
            {
                return NoContent();
            }

            strategy.Execute(() => {
                try {
                    _unitOfWork.GenericMethods.BeginTransaction();

                    List<UserTagsDM> tags = new List<UserTagsDM>();

                    foreach (TagDM tag in tagRequest.tags)
                    {
                        tags.Add(new UserTagsDM() {
                            UserId = tagRequest.UserId,
                            TagId = tag.TagId
                        });
                    }

                    _unitOfWork.UserTags.AddRange(tags);

                    _unitOfWork.Save();

                    _unitOfWork.GenericMethods.CommitTransaction();
                } catch (Exception exception) {
                    Console.WriteLine(exception);
                    _unitOfWork.GenericMethods.RollBackTransaction();
                }
            });

            return Ok(GetTags(tagRequest.UserId));
        }

        [HttpGet("{userId}/Group")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupDM)))]
        public IActionResult UserGroup(string userId)
        {
            var user = _unitOfWork.Users.GetById(userId);

            if (user == null) {
                return NotFound();
            }

            var group = _unitOfWork.UserGroups
                .Get(
                    include: (table) => table.Include(table => table.Group),
                    filter: (table) => table.UserId == userId
                )
                .Select((table) => table.Group);


            return Ok(group);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserDTO)))]
        public IActionResult Get(string id)
        {
            User user = _unitOfWork.Users.GetById(id);
            if (user == null)
                return NotFound();
            else
            {
                UserDTO userConvert = new UserDTO
                {
                    UserId = user.Id,
                    NormalizedEmail = user.NormalizedEmail,
                    NormalizedUserName = user.NormalizedUserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ProfilePicture = user.ProfilePicture,
                    TwoFactorEnabled = user.TwoFactorEnabled
                };
                return Ok(userConvert);
            }
        }

        [HttpGet("{id}/Recommended")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserGroupDM)))]
        public IActionResult GetRecommendedUsers(string id)
        {
            var cm = new UserCM(_unitOfWork, _logger);

            if (cm == null)
                return NotFound();
            else
                return Ok(cm.GetRecommendUsers(id));
        }

        [HttpGet("{id}/Recommended/Groups")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupTagDM)))]
        public IActionResult GetRecommendedGroups(string id)
        {
            var cm = new UserCM(_unitOfWork, _logger);

            if (cm == null)
                return NotFound();
            else
                return Ok(cm.GetRecommendGroups(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserDTO)))]
        public IActionResult Post([FromBody] UserDTO user)
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            User userConvert = new User 
            {
                Id = user.UserId,
                NormalizedUserName = user.NormalizedUserName,
                NormalizedEmail = user.NormalizedEmail,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicture = user.ProfilePicture,
                TwoFactorEnabled = user.TwoFactorEnabled
            };

            _unitOfWork.Users.AddOrUpdate(userConvert);
            _unitOfWork.Save();

            return Ok(user);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserDTO)))]
        public IActionResult Put(string id, [FromBody] UserDTO user)
        {
            if (user == null)
                return BadRequest();

            if (id != user.UserId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            User userConvert = new User
            {
                Id = user.UserId,
                NormalizedUserName = user.NormalizedUserName,
                NormalizedEmail = user.NormalizedEmail,
                ProfilePicture = user.ProfilePicture,
                FirstName = user.FirstName,
                LastName= user.LastName,
                TwoFactorEnabled = user.TwoFactorEnabled
            };

            _unitOfWork.Users.Update(userConvert);
            _unitOfWork.Save();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserDTO)))]
        public IActionResult Delete(string id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user == null)
                return NotFound();

            user.IsDeleted = true;
            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();

            UserDTO userConvert = new UserDTO
            {
                UserId = user.Id,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                ProfilePicture = user.ProfilePicture,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TwoFactorEnabled = user.TwoFactorEnabled
            };

            return Ok(userConvert);
        }
    }
}
