using marking_api.API.Config;
using marking_api.API.Models.Config;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace marking_api.API.Controllers.Config
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public QueryController(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        [HttpPost]
        [ClaimRequirement(MarkingClaimTypes.Permission, "DatabaseQuery")]
        public IActionResult Query([FromBody] string sql)
        {
            if (sql == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            List<dynamic> result = _unitOfWork.GenericMethods.ExecuteQuery(sql).ToList();

            if (result != null)
                return Ok(result);
            else
                return BadRequest("Incorrect SQL");
        }
    }
}
