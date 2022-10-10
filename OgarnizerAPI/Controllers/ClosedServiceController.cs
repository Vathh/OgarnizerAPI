using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;

namespace OgarnizerAPI.Controllers
{
    [Route("api/ogarnizer/closedservice")]
    [ApiController]
    //[Authorize]
    public class ClosedServiceController : ControllerBase
    {
        private readonly IClosedServiceService _closedServiceService;

        public ClosedServiceController(IClosedServiceService closedServiceService)
        {
            _closedServiceService = closedServiceService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClosedServiceDto>> GetAll([FromQuery] ClosedServiceQuery query)
        {
            var closedServicesDtos = _closedServiceService.GetAll(query);

            return Ok(closedServicesDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ClosedServiceDto> Get([FromRoute] int id)
        {
            var closedService = _closedServiceService.GetById(id);

            return Ok(closedService);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateClosedServiceDto dto)
        {
            _closedServiceService.Update(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _closedServiceService.Delete(id);

            return NoContent();
        }
    }
}
