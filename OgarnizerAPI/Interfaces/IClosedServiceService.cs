using OgarnizerAPI.Models;

namespace OgarnizerAPI.Interfaces
{
    public interface IClosedServiceService
    {
        PagedResult<ClosedServiceDto> GetAll(ClosedServiceQuery query);
        ClosedServiceDto? GetById(int id);
        void Update(int id, UpdateClosedServiceDto dto);
        void Delete(int id);
    }
}
