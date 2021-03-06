using log4net;
using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.Project
{
    /// <summary>
    /// GroupMarker API Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GroupMarkerController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Constructor initialising unitofwork
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public GroupMarkerController(IUnitOfWork unitOfWork, ILog logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GET GroupMarkers
        /// </summary>
        /// <returns>List of GroupMarkerDM</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupMarkerDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.GroupMarkers.Get());
        }

        /// <summary>
        /// GET groupmarker by id
        /// </summary>
        /// <param name="id">long</param>
        /// <returns>GroupMarkerDM</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupMarkerDM)))]
        public IActionResult Get(long id)
        {
            var groupMarker = _unitOfWork.GroupMarkers.GetById(id);
            if (groupMarker == null)
                return NotFound();
            else
                return Ok(groupMarker);
        }

        /// <summary>
        /// POST a GroupMarker
        /// </summary>
        /// <param name="groupMarker">GroupMarkerDM</param>
        /// <returns>Saved GroupMarkerDM</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupMarkerDM)))]
        public IActionResult Post([FromBody] GroupMarkerDM groupMarker)
        {
            if (groupMarker == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.GroupMarkers.AddOrUpdate(groupMarker);
            _unitOfWork.Save();

            return Ok(groupMarker);
        }

        /// <summary>
        /// PUT a GroupMarkerDM by id
        /// </summary>
        /// <param name="id">Int64 GroupMarker id</param>
        /// <param name="groupMarker">GroupMarkerDM</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupMarkerDM)))]
        public IActionResult Put(long id, [FromBody] GroupMarkerDM groupMarker)
        {
            if (groupMarker == null)
                return BadRequest();

            if (id != groupMarker.GroupMarkerId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.GroupMarkers.Update(groupMarker);
            _unitOfWork.Save();

            return Ok(groupMarker);
        }

        /// <summary>
        /// DELETE a GroupMarkerDM by id
        /// </summary>
        /// <param name="id">long</param>
        /// <returns>Deleted GroupMarkerDM</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GroupMarkerDM)))]
        public IActionResult Delete(long id)
        {
            var groupMarker = _unitOfWork.GroupMarkers.GetById(id);
            if (groupMarker == null)
                return NotFound();

            groupMarker.deleted = true;
            _unitOfWork.GroupMarkers.Update(groupMarker);
            _unitOfWork.Save();

            return Ok(groupMarker);
        }
    }
}