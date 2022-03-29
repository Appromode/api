using log4net.Core;
using marking_api.DataModel.DTOs;
using marking_api.DataModel.Identity;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using marking_api.Global.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinkController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UtilService _utilService;

        public LinkController(IUnitOfWork unitOfWork, UtilService utilService, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
            _utilService = utilService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(LinkDTO)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Links.Get().ToList().ToLinkDTOList());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(LinkDTO)))]
        public IActionResult Get(Int64 id)
        {
            var link = _unitOfWork.Links.GetById(id).ToLinkDTO();
            if (link == null)
                return NotFound();
            else            
                return Ok(link);
        }

        [HttpGet("GetUserMenu")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(LinkDTO)))]
        public IActionResult GetUserMenu(string userName)
        {
            if (userName == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            List<LinkDM> links = _utilService.GenerateUserMenu(userName);

            return Ok(links);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(LinkDTO)))]
        public IActionResult Post([FromBody] LinkDTO link)
        {
            if (link == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Links.AddOrUpdate(link.ToLinkDM());
            _unitOfWork.Save();

            return Ok(link);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(LinkDTO)))]
        public IActionResult Put(Int64 id, [FromBody] LinkDTO link)
        {
            if (link == null)
                return BadRequest();

            if (id != link.LinkId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Links.Update(link.ToLinkDM());
            _unitOfWork.Save();

            return Ok(link);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(LinkDTO)))]
        public IActionResult Delete(Int64 id)
        {
            var link = _unitOfWork.Links.GetById(id);
            if (link == null)
                return NotFound();

            _unitOfWork.Links.Delete(link);
            _unitOfWork.Save();

            return Ok(link.ToLinkDTO());
        }
    }
}
