using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;

namespace OgarnizerAPI.Controllers
{
    [Route("api/ogarnizer/service")]
    [ApiController]
    //[Authorize]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpPost]
        public ActionResult CreateService([FromBody] CreateServiceDto dto)
        {
            var id = _serviceService.Create(dto);

            return Created($"/api/ogarnizer/service/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ServiceDto>> GetAll([FromQuery] ServiceQuery query)
        {
            var serviceDtos = _serviceService.GetAll(query);

            return Ok(serviceDtos);
        }

        [HttpGet("{id}")]
        //[AllowAnonymous]
        public ActionResult<ServiceDto> Get([FromRoute] int id)
        {
            var service = _serviceService.GetById(id);

            return Ok(service);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateServiceDto dto)
        {
            _serviceService.Update(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _serviceService.Delete(id);

            return NoContent();
        }

        [HttpPost("{id}")]
        public ActionResult CloseJob([FromRoute] int id, [FromQuery] bool isDone)
        {
            _serviceService.Close(id, isDone);

            return Ok();
        }


    }
}
