using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;

namespace OgarnizerAPI.Controllers
{
    [Route("api/ogarnizer/closedorder")]
    [ApiController]
    //[Authorize]
    public class ClosedOrderController : ControllerBase
    {
        private readonly IClosedOrderService _closedOrderService;

        public ClosedOrderController(IClosedOrderService closedOrderService)
        {
            _closedOrderService = closedOrderService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClosedOrderDto>> GetAll([FromQuery] ClosedOrderQuery query)
        {
            var closedOrdersDtos = _closedOrderService.GetAll(query);

            return Ok(closedOrdersDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ClosedOrderDto> Get([FromRoute] int id)
        {
            var closedOrder = _closedOrderService.GetById(id);

            return Ok(closedOrder);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateClosedOrderDto dto)
        {
            _closedOrderService.Update(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _closedOrderService.Delete(id);

            return NoContent();
        }
    }
}
