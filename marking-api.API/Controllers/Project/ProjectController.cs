using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ProjectDM)))]
        public IActionResult Post([FromBody] ProjectDM project)
        {
            if (project == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Projects.Update(project);
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
    }
}
