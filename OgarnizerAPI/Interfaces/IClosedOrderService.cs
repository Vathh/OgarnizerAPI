using OgarnizerAPI.Models;

namespace OgarnizerAPI.Interfaces
{
    public interface IClosedOrderService
    {
        PagedResult<ClosedOrderDto> GetAll(ClosedOrderQuery query);
        ClosedOrderDto? GetById(int id);
        void Update(int id, UpdateClosedOrderDto dto);
        void Delete(int id);
    }
}
