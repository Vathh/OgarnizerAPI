using OgarnizerAPI.Models;

namespace OgarnizerAPI.Interfaces
{
    public interface IClosedJobService
    {
        PagedResult<ClosedJobDto> GetAll(JobQuery query);
        ClosedJobDto? GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateClosedJobDto dto);
    }
}
