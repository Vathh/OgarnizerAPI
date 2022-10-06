using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Exceptions;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;

namespace OgarnizerAPI.Services
{
    public class ClosedJobService : IClosedJobService
    {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CA2254 // Template should be a static expression
        private readonly OgarnizerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ClosedJobService> _logger;
        private readonly IUserContextService _userContextService;

        public ClosedJobService(OgarnizerDbContext dbContext,
                                IMapper mapper,
                                ILogger<ClosedJobService> logger,
                                IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
        }

        public ClosedJobDto? GetById(int id)
        {
            var closedJob = _dbContext
                .ClosedJobs
                .FirstOrDefault(x => x.Id == id);

            if(closedJob == null)
            {
                throw new NotFoundException("ClosedJob not found");
            };

            var result = _mapper.Map<ClosedJobDto>(closedJob);

            return result;
        }

        public PagedResult<ClosedJobDto> GetAll(ClosedJobQuery query)
        {
            var baseQuery = _dbContext
                            .ClosedJobs
                            .Include(r => r.CloseUser)
                            .Where(r => query.SearchPhrase == null ||
                                    (r.Place.ToLower().Contains(query.SearchPhrase.ToLower()) || r.CreatedDate.Equals(query.SearchPhrase)));

            if (!string.IsNullOrEmpty(query.SortBy))
            {

                var columnsSelectors = new Dictionary<string, Expression<Func<ClosedJob, object>>>
                {
                    {nameof(ClosedJob.Place), r => r.Place},
                    {nameof(ClosedJob.Object), r => r.Object},
                    {nameof(ClosedJob.CreatedDate), r => r.CreatedDate},
                    {nameof(ClosedJob.UpdateDate), r => r.UpdateDate}
                };


                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                                    baseQuery.OrderBy(selectedColumn)
                                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var closedJobs = baseQuery
                        .Skip(query.PageSize * (query.PageNumber - 1))
                        .Take(query.PageSize)
                        .ToList();

            var totalItemsCount = baseQuery.Count();

            var closedJobsDtos = _mapper.Map<List<ClosedJobDto>>(closedJobs);

            var result = new PagedResult<ClosedJobDto>(closedJobsDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }        

        public void Update(int id, UpdateClosedJobDto dto)
        {
            var closedJob = _dbContext
                .ClosedJobs
                .FirstOrDefault(x => x.Id == id);

            if (closedJob is null)
            {
                throw new NotFoundException("Job not found");
            }


            closedJob.UpdateInfo = dto.UpdateInfo;
            closedJob.UpdateDate = dto.UpdateDate;
            _dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var message = $"ClosedJob with id: {id} DELETE action invoked";

            _logger.LogError(message);


            var closedJob = _dbContext
                .ClosedJobs
                .FirstOrDefault(x => x.Id == id);

            if (closedJob is null)
            {
                throw new NotFoundException("Job not found");
            }

            _dbContext.ClosedJobs.Remove(closedJob);
            _dbContext.SaveChanges();
        }
    }
}

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CA2254 // Template should be a static expression
