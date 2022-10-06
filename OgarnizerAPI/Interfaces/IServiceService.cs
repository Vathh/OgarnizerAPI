using OgarnizerAPI.Models;

namespace OgarnizerAPI.Interfaces
{
    public interface IServiceService
    {
        int Create(CreateServiceDto dto);
        PagedResult<ServiceDto> GetAll(ServiceQuery query);
        ServiceDto? GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateServiceDto dto);
        void Close(int id, bool isDone);
    }
}
