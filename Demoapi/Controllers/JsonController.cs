using Microsoft.AspNetCore.Mvc;
using Demoapi.Models;
using Microsoft.AspNetCore.Authorization;
using Demoapi.Interface;


namespace Demoapi.Controllers
{
    [Authorize]
    [Route("api/jsondata")]
    [ApiController]
    public class JsonController : ControllerBase
    {
        private readonly IJsonRepository _jsonRepository;
        // private jsondata _randomData = null;

        [AllowAnonymous]
        [HttpGet("randomdata")]
        public IActionResult GetRandomData()
        {
            var random = new Random();
            var x = new List<int>();
            var y1 = new List<int>();
            var y2 = new List<int>();

            for (int i = 0; i < 15; i++)
            {
                x.Add(i);
                y1.Add(random.Next(50, 100));
                y2.Add(random.Next(50, 100));
            }

            var randomData = new
            {
                x,
                y1,
                y2
            };

            return Ok(randomData);
        }

        public JsonController(IJsonRepository jsonRepository)
        {
            _jsonRepository = jsonRepository;
        }

        [HttpGet("{JsonId}")]
        public IActionResult GetJson(Guid JsonId)
        {
            var json = _jsonRepository.GetJson(JsonId);

            if (json == null)
            {
                return NotFound();
            }

            return Ok(json);
        }

        [HttpPost]
        public IActionResult CreateJson([FromBody] jsondata json)
        {
            if (json == null)
            {
                return BadRequest();
            }

            _jsonRepository.CreateJson(json);
            _jsonRepository.Save();

            return CreatedAtAction("GetJson", new { JsonId = json.JsonId }, json);
        }

        [HttpPut("{JsonId}")]
        public IActionResult UpdateJson(Guid JsonId, [FromBody] jsondata json)
        {
            if (json == null || JsonId != json.JsonId)
            {
                return BadRequest();
            }

            if (!_jsonRepository.JsonExists(JsonId))
            {
                return NotFound();
            }

            _jsonRepository.UpdateJson(JsonId);
            _jsonRepository.Save();

            return NoContent();
        }

        [HttpDelete("{JsonId}")]
        public IActionResult DeleteJson(Guid JsonId)
        {
            if (!_jsonRepository.JsonExists(JsonId))
            {
                return NotFound();
            }

            _jsonRepository.DeleteJson(JsonId);
            _jsonRepository.Save();

            return NoContent();
        }


    }
}
