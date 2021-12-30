using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using marking_api.DataModel.Identity;
using marking_api.Global.Extensions;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(Role)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Roles.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(Role)))]
        public IActionResult Get(string id)
        {
            var role = _unitOfWork.Roles.GetById(id);
            if (role == null)
                return NotFound();
            else
                return Ok(role);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(Role)))]
        public IActionResult Post([FromBody] Role role)
        {
            if (role == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Roles.AddOrUpdate(role);
            _unitOfWork.Save();

            return Ok(role);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(Role)))]
        public IActionResult Put(string id, [FromBody] Role role)
        {
            if (role == null)
                return BadRequest();

            if (id != role.Id)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Roles.Update(role);
            _unitOfWork.Save();

            return Ok(role);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(Role)))]
        public IActionResult Delete(string id)
        {
            var role = _unitOfWork.Roles.GetById(id);
            if (role == null)
                return NotFound();

            _unitOfWork.Roles.Delete(role);
            _unitOfWork.Save();

            return Ok(role);
        }
    }
}
