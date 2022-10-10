using OgarnizerAPI.Models;

namespace OgarnizerAPI.Interfaces
{
    public interface IOrderService
    {
        int Create(CreateOrderDto dto);
        PagedResult<OrderDto> GetAll(OrderQuery query);
        OrderDto? GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateOrderDto dto);

        void Close(int id, bool isDone);
    }
}
