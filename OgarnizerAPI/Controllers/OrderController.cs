using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;
using OgarnizerAPI.Services;

namespace OgarnizerAPI.Controllers
{
    [Route("api/ogarnizer/order")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public ActionResult CreateOrder([FromBody] CreateOrderDto dto)
        {
            var id = _orderService.Create(dto);

            return Created($"/api/ogarnizer/order/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetAll([FromQuery] OrderQuery query)
        {
            var ordersDtos = _orderService.GetAll(query);

            return Ok(ordersDtos);
        }

        [HttpGet("{id}")]
        //[AllowAnonymous]
        public ActionResult<OrderDto> Get([FromRoute] int id)
        {
            var order = _orderService.GetById(id);

            return Ok(order);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateOrderDto dto)
        {
            _orderService.Update(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _orderService.Delete(id);

            return NoContent();
        }

        [HttpPost("{id}")]
        public ActionResult CloseOrder([FromRoute] int id, [FromQuery] bool isDone)
        {
            _orderService.Close(id, isDone);

            return Ok();
        }
    }
}
