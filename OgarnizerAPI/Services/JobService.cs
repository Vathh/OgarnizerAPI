using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Exceptions;
using OgarnizerAPI.Models;

namespace OgarnizerAPI.Services
{
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CA2254 // Template should be a static expression
    public class JobService : IJobService
    {
        private readonly OgarnizerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<JobService> _logger;
        private readonly IUserContextService _userContextService;

        public JobService(OgarnizerDbContext dbContext, IMapper mapper, ILogger<JobService> logger, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
        }
        public int Create(CreateJobDto dto)
        {
            var job = _mapper.Map<Job>(dto);
            _dbContext.Jobs.Add(job);
            _dbContext.SaveChanges();

            return job.Id;
        }
        public JobDto? GetById(int id)
        {
            var job = _dbContext
                .Jobs
                .FirstOrDefault(x => x.Id == id);

            if (job is null)
            {
                throw new NotFoundException("Job not found");
            };

            var result = _mapper.Map<JobDto>(job);

            return result;
        }

        public PagedResult<JobDto> GetAll(JobQuery query)
        {

            var baseQuery = _dbContext
                            .Jobs
                            .Include(r => r.User)
                            .Where(r => query.SearchPhrase == null ||
                                        (r.Place.ToLower().Contains(query.SearchPhrase.ToLower()) || r.CreatedDate.Equals(query.SearchPhrase)));


            if (!string.IsNullOrEmpty(query.SortBy))
            {

                var columnsSelectors = new Dictionary<string, Expression<Func<Job, object>>>
                {
                    {nameof(Job.Place), r => r.Place},
                    {nameof(Job.Description), r => r.Description},
                    {nameof(Job.Object), r => r.Object}
                };


                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                                    baseQuery.OrderBy(selectedColumn)
                                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var jobs = baseQuery
                        .Skip(query.PageSize * (query.PageNumber - 1))
                        .Take(query.PageSize)
                        .ToList();

            var totalItemsCount = baseQuery.Count();

            var jobsDtos = _mapper.Map<List<JobDto>>(jobs);

            var result = new PagedResult<JobDto>(jobsDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }
        public void Update(int id, UpdateJobDto dto)
        {

            var job = _dbContext
                .Jobs
                .FirstOrDefault(x => x.Id == id);

            if (job is null)
            {
                throw new NotFoundException("Job not found");
            }


            job.UpdateInfo = dto.UpdateInfo;
            _dbContext.SaveChanges();
        }
        public void Delete(int id) 
        {
            var message = $"Job with id: {id} DELETE action invoked";

            _logger.LogError(message);


            var job = _dbContext
                .Jobs
                .FirstOrDefault(x => x.Id == id);

            if (job is null)
            {
                throw new NotFoundException("Job not found");
            }

            _dbContext.Jobs.Remove(job);
            _dbContext.SaveChanges();
        }
        public void Close(int id, bool isDone)
        {
            var job = _dbContext
                .Jobs
                .FirstOrDefault(x => x.Id == id);

            if (job is null)
            {
                throw new NotFoundException("Job not found");
            }

            var closedJob = new ClosedJob
            {
                UserId = job.UserId,
                CreatedDate = job.CreatedDate,
                Priority = job.Priority,
                Description = job.Description,
                Place = job.Place,
                Object = job.Object,
                AdditionalInfo = job.AdditionalInfo,
                UpdateInfo = job.UpdateInfo,
                IsDone = isDone,
                ClosedDate = DateTime.Now,
                CloseUserId = _userContextService.GetUserId
            };
            
            _dbContext.Jobs.Remove(job);
            _dbContext.ClosedJobs.Add(closedJob);
            _dbContext.SaveChanges();
        }
    }
}

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CA2254 // Template should be a static expression
