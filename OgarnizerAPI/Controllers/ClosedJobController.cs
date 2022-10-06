using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;

namespace OgarnizerAPI.Controllers
{
    [Route("api/ogarnizer/closedjob")]
    [ApiController]
    //[Authorize]
    public class ClosedJobController : ControllerBase
    {
        private readonly IClosedJobService _closedJobService;

        public ClosedJobController(IClosedJobService closedJobService)
        {
            _closedJobService = closedJobService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClosedJobDto>> GetAll([FromQuery] ClosedJobQuery query)
        {
            var closedJobsDtos = _closedJobService.GetAll(query);

            return Ok(closedJobsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ClosedJobDto> Get([FromRoute] int id)
        {
            var closedJob = _closedJobService.GetById(id);

            return Ok(closedJob);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateClosedJobDto dto)
        {
            _closedJobService.Update(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _closedJobService.Delete(id);

            return NoContent();
        }        
    }
}
