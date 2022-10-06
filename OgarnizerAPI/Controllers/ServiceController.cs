using Microsoft.AspNetCore.Mvc;
using OgarnizerAPI.Interfaces;

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

        
    }
}
