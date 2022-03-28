using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.Project
{
    /// <summary>
    /// UserGroup API Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserGroupController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Constructor initialising unitofwork
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public UserGroupController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GET List of UserGroupDM
        /// </summary>
        /// <returns>List of UserGroupDM</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserGroupDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.UserGroups.Get());
        }

        /// <summary>
        /// GET UserGroupDM by id
        /// </summary>
        /// <param name="id">Int64</param>
        /// <returns>UserGroupDM</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserGroupDM)))]
        public IActionResult Get(long id)
        {
            var userGroup = _unitOfWork.UserGroups.GetById(id);
            if (userGroup == null)
                return NotFound();
            else
                return Ok(userGroup);
        }

        /// <summary>
        /// POST UserGroupDM
        /// </summary>
        /// <param name="userGroup"></param>
        /// <returns>Saved or updated UserGroupDM</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserGroupDM)))]
        public IActionResult Post([FromBody] UserGroupDM userGroup)
        {
            if (userGroup == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserGroups.AddOrUpdate(userGroup);
            _unitOfWork.Save();

            return Ok(userGroup);
        }

        /// <summary>
        /// PUT UserGroupDM by id
        /// </summary>
        /// <param name="id">Int64</param>
        /// <param name="userGroup">UserGroupDM</param>
        /// <returns>Updated UserGroupDM</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserGroupDM)))]
        public IActionResult Put(long id, [FromBody] UserGroupDM userGroup)
        {
            if (userGroup == null)
                return BadRequest();

            if (id != userGroup.UserGroupId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserGroups.Update(userGroup);
            _unitOfWork.Save();

            return Ok(userGroup);
        }

        /// <summary>
        /// DELETE UserGroupDM
        /// </summary>
        /// <param name="id">Int64</param>
        /// <returns>Deleted UserGroupDM</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserGroupDM)))]
        public IActionResult Delete(long id)
        {
            var userGroup = _unitOfWork.UserGroups.GetById(id);
            if (userGroup == null)
                return NotFound();

            userGroup.deleted = true;
            _unitOfWork.UserGroups.Update(userGroup);
            _unitOfWork.Save();

            return Ok(userGroup);
        }
    }
}
