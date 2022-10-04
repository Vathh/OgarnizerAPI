using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Exceptions;
using OgarnizerAPI.Models;

namespace OgarnizerAPI.Services
{
    public class JobService : IJobService
    {
        private readonly OgarnizerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<JobService> _logger;
        //private readonly IAuthorizationService _authorizationService;
        //private readonly IUserContextService _userContextService;

        public JobService(OgarnizerDbContext dbContext, IMapper mapper, ILogger<JobService> logger)
            // + IAuthorizationService authorizationService
            // + IUserContextService userContextService
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            // _authorizationService = authorizationService;
            // _userContextService = userContextService;
        }
#pragma warning disable CS8604 // Possible null reference argument.
        public void Delete(int id) 
        {
            var message = $"Job with id: {id} DELETE action invoked";
#pragma warning disable CA2254 // Template should be a static expression
            _logger.LogError(message);
#pragma warning restore CA2254 // Template should be a static expression

            var job = _dbContext
                .Jobs
                .FirstOrDefault(x => x.Id == id);

            if (job is null)
            {
                throw new NotFoundException("Job not found");
            }

            // var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User , job, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            //if(!authorizationResult.Succeeded)
            // throw new ForbidException();

            _dbContext.Jobs.Remove(job);
            _dbContext.SaveChanges();
        }

        public void Update(int id, UpdateJobDto dto) 
        {

            var job = _dbContext
                .Jobs
                .FirstOrDefault(x => x.Id == id);

            if  (job is null) 
            { 
                throw new NotFoundException("Job not found"); 
            }

            // var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User , job, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            //if(!authorizationResult.Succeeded)
            // throw new ForbidException();


            job.UpdateInfo = dto.UpdateInfo;
            _dbContext.SaveChanges();
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
#pragma warning restore CS8604 // Possible null reference argument.

            if (!string.IsNullOrEmpty(query.SortBy))
            {
#pragma warning disable CS8603 // Possible null reference return.
                var columnsSelectors = new Dictionary<string, Expression<Func<Job, object>>> 
                {
                    {nameof(Job.Place), r => r.Place},
                    {nameof(Job.Description), r => r.Description},
                    {nameof(Job.Object), r => r.Object}
                };
#pragma warning restore CS8603 // Possible null reference return.

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ? 
                                    baseQuery.OrderBy(selectedColumn) 
                                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var jobs = baseQuery
                        .Skip(query.PageSize * (query.PageNumber -1))
                        .Take(query.PageSize)
                        .ToList();

            var totalItemsCount = baseQuery.Count();

            var jobsDtos = _mapper.Map<List<JobDto>>(jobs);

            var result = new PagedResult<JobDto>(jobsDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public int Create(CreateJobDto dto)
        {
            var job = _mapper.Map<Job>(dto);
            //job.UserId = _userContextService.GetUserId;
            _dbContext.Jobs.Add(job);
            _dbContext.SaveChanges();

            return job.Id;
        }

    }
}
