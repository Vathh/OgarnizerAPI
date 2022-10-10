using OgarnizerAPI.Models;

namespace OgarnizerAPI.Services
{
    public interface IJobService
    {
        int Create(CreateOrderDto dto);
        PagedResult<JobDto> GetAll(OrderQuery query);
        JobDto? GetById(int id);    
        void Delete(int id);    
        void Update(int id, UpdateOrderDto dto); 

        void Close(int id, bool isDone);
    }
}