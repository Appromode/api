using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Add([FromBody] GroupDM group)
        {
            if (group == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Groups.Add(group);
            _unitOfWork.Save();

            return Ok(group);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupDM)))]
        public IActionResult Update(long id, [FromBody] GroupDM group)
        {
            if (group == null)
                return BadRequest();

            if (id != group.GroupId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Groups.Update(group);
            _unitOfWork.Save();

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
