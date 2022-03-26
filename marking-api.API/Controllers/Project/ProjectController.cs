﻿using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace marking_api.API.Controllers.Project
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ProjectDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Projects.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ProjectDM)))]
        public IActionResult Get(long id)
        {
            var project = _unitOfWork.Projects.GetById(id);
            if (project == null)
                return NotFound();
            else
                return Ok(project);
        }

        [HttpGet("{projectId}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ProjectDM)))]
        public IActionResult Get(int projectId)
        {
            //var projectComments = _unitOfWork.Projects.Get(filter: x => x.ProjectId == projectId,
            //    include: x => x.Include(y => y.Comments).ThenInclude(y => y.User));

            var projectComments = _unitOfWork.Projects.Get(
                include: (projectDM) => projectDM
                    .Include((projectDM) => projectDM.Comments)
                    .ThenInclude((projectComments) => projectComments.User),
                filter: (projectDM) => projectDM.ProjectId == projectId);

            return Ok(projectComments);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ProjectDM)))]
        public IActionResult Post([FromBody] ProjectDM project)
        {
            if (project == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Projects.AddOrUpdate(project);
            _unitOfWork.Save();

            return Ok(project);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ProjectDM)))]
        public IActionResult Put(long id, [FromBody] ProjectDM project)
        {
            if (project == null)
                return BadRequest();

            if (id != project.ProjectId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Projects.Update(project);
            _unitOfWork.Save();

            return Ok(project);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ProjectDM)))]
        public IActionResult Delete(long id)
        {
            var project = _unitOfWork.Projects.GetById(id);
            if (project == null)
                return NotFound();

            project.deleted = true;
            _unitOfWork.Projects.Update(project);
            _unitOfWork.Save();

            return Ok(project);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ProjectDM)))]
        public IActionResult Patch(long id, [FromBody] JsonPatchDocument<ProjectDM> patchEntity)
        {
            if (patchEntity != null)
            {
                var project = _unitOfWork.Projects.GetById(id);

                patchEntity.ApplyTo(project, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                _unitOfWork.Projects.Update(project);
                _unitOfWork.Save();
                return Ok(project);
            }
            else
            {
                return BadRequest(ModelState);
            }
    }
}
}
