using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<T> : ControllerBase where T : class
    {
        protected IGenericModelRepository<T> _genericModelRepository;

        public BaseController(IGenericModelRepository<T> genericModelRepository)
        {
            _genericModelRepository = genericModelRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(_genericModelRepository.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var obj = _genericModelRepository.GetById(id);
            if (obj == null)
                return NotFound();
            else
                return new ObjectResult(obj);
        }

        [HttpPost]
        public IActionResult Post([FromBody] T obj)
        {
            if (obj == null)
                return BadRequest();
            _genericModelRepository.Add(obj);
            _genericModelRepository.Save();

            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] T obj)
        {
            if (obj == null)
                return BadRequest();

            _genericModelRepository.Update(obj);
            _genericModelRepository.Save();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _genericModelRepository.Delete(id);
            _genericModelRepository.Save();

            return Ok();
        }
    }
}
