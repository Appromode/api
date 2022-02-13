using marking_api.API.Models.Project;
using marking_api.DataModel.API;
using marking_api.DataModel.Identity;
using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace marking_api.API.Controllers.Project
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GroupController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Groups.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupDM)))]
        public IActionResult Get(long id)
        {
            var group = _unitOfWork.Groups.GetById(id);
            if (group == null)
                return NotFound();
            else
                return Ok(group);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupDM)))]
        public IActionResult Post(GroupRequest groupReq)
        {
            if (groupReq == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            GroupDM group = _unitOfWork.Groups.Add(new GroupDM {
                GroupName = groupReq.GroupName
            });
            _unitOfWork.Save();

            List<UserGroupDM> userGroups = new List<UserGroupDM>();
            
            foreach (var user in groupReq.GroupMembers)
            {
                userGroups.Add(new UserGroupDM 
                {
                    UserId = user.Id,
                    GroupId = group.GroupId
                });
            }

            _unitOfWork.UserGroups.AddRange(userGroups);
            _unitOfWork.Save();

            group.GroupUsers = new List<UserGroupDM>();
            group.GroupUsers = _unitOfWork.UserGroups.Get(filter: x => x.GroupId == group.GroupId).ToList();

            return Ok(group);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupDM)))]
        public IActionResult Put(long id, [FromBody] GroupDM group)
        {
            if (group == null)
                return BadRequest();

            if (id != group.GroupId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var cm = new GroupCM(_unitOfWork);

            cm.GenerateGroup(group);
            //_unitOfWork.Groups.Update(group);
            //_unitOfWork.Save();

            return Ok(group);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupDM)))]
        public IActionResult Delete(long id)
        {
            var group = _unitOfWork.Groups.GetById(id);
            if (group == null)
                return NotFound();

            group.deleted = true;
            _unitOfWork.Groups.Update(group);
            _unitOfWork.Save();

            return Ok(group);
        }
    }
}
