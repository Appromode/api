using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using marking_api.Global.Extensions;
using marking_api.DataModel.DTOs;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserDTO)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Users.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserDTO)))]
        public IActionResult Get(string id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user == null)
                return NotFound();
            else
                return Ok(user);
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserDTO)))]
        //public IActionResult Post([FromBody] UserDTO user)
        //{
        //    if (user == null)
        //        return BadRequest();

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrorMessages());

        //    _unitOfWork.Users.Update(user);
        //    _unitOfWork.Save();

        //    return Ok(user);
        //}

        //[HttpPut]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserDTO)))]
        //public IActionResult Put(string id, [FromBody] UserDTO user)
        //{
        //    if (user == null)
        //        return BadRequest();

        //    if (id != user.Id)
        //        return BadRequest("Id Mismatch");

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrorMessages());

        //    _unitOfWork.Users.Update(user);
        //    _unitOfWork.Save();

        //    return Ok(user);
        //}

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

            return Ok(user);
        }
    }
}
