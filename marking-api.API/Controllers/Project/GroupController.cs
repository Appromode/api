using marking_api.API.Models.Project;
using marking_api.DataModel.Identity;
using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;

namespace marking_api.API.Controllers.Project
{
    public class GroupRequest {
        public string GroupName;
        public string GroupDescription;
        public List<User> Users; 
    }

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
        //[ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupRequest)))]
        public IActionResult Post(GroupRequest group)
        {
            if (group == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            GroupDM groupDM = _unitOfWork.Groups.Add(new GroupDM {
                GroupName = group.GroupName,
            });

            List<UserGroupDM> userGroupList = new List<UserGroupDM>();
            
            group.Users.ForEach((user) => userGroupList.Add(new UserGroupDM() {
              UserId = user.Id,
              GroupId = groupDM.GroupId,
            }));

            _unitOfWork.UserGroups.AddOrUpdateRange(userGroupList);

            _unitOfWork.Save();

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
