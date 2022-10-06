using OgarnizerAPI.Models;

namespace OgarnizerAPI.Interfaces
{
    public interface IClosedJobService
    {
        PagedResult<ClosedJobDto> GetAll(ClosedJobQuery query);
        ClosedJobDto? GetById(int id);
        void Update(int id, UpdateClosedJobDto dto);
        void Delete(int id);
    }
}
