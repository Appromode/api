using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using marking_api.DataModel.Project;
using marking_api.API.Models.Recommend;
using Microsoft.AspNetCore.Mvc;
using marking_api.Global.Extensions;
using marking_api.DataModel.DTOs;
using marking_api.DataModel.Identity;

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

        [HttpGet("{id}/Invites")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserDTO)))]
        public IActionResult GetInvites(string id)
        {
            var cm = new UserCM(_unitOfWork);

            if (cm != null) {
                return Ok(cm.GetInvites(id));
            }
            return NoContent();
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
        public IActionResult GetRecommended(string id)
        {
            var cm = new RecommendCM(_unitOfWork);

            if (cm == null)
                return NotFound();
            else
                return Ok(cm.GetRecommendUsers(id));
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
