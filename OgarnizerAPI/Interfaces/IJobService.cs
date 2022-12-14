using OgarnizerAPI.Models;

namespace OgarnizerAPI.Interfaces
{
    public interface IJobService
    {
        int Create(CreateJobDto dto);
        PagedResult<JobDto> GetAll(JobQuery query);
        JobDto? GetById(int id);    
        void Delete(int id);    
        void Update(int id, UpdateJobDto dto); 

        void Close(int id, bool isDone);
    }
}