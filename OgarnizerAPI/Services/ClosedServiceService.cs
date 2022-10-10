using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Exceptions;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;
using System.Linq.Expressions;

namespace OgarnizerAPI.Services
{
    public class ClosedServiceService : IClosedServiceService
    {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CA2254 // Template should be a static expression
        private readonly OgarnizerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ClosedServiceService> _logger;
        private readonly IUserContextService _userContextService;

        public ClosedServiceService(OgarnizerDbContext dbContext,
                                IMapper mapper,
                                ILogger<ClosedServiceService> logger,
                                IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
        }

        public ClosedServiceDto? GetById(int id)
        {
            var closedService = _dbContext
                .ClosedServices
                .FirstOrDefault(x => x.Id == id);

            if (closedService == null)
            {
                throw new NotFoundException("ClosedService not found");
            };

            var result = _mapper.Map<ClosedServiceDto>(closedService);

            return result;
        }

        public PagedResult<ClosedServiceDto> GetAll(ClosedServiceQuery query)
        {
            var baseQuery = _dbContext
                            .ClosedServices
                            .Include(r => r.CloseUser)
                            .Where(r => query.SearchPhrase == null ||
                                    (r.Object.ToLower().Contains(query.SearchPhrase.ToLower()) || r.CreatedDate.Equals(query.SearchPhrase)));

            if (!string.IsNullOrEmpty(query.SortBy))
            {

                var columnsSelectors = new Dictionary<string, Expression<Func<ClosedService, object>>>
                {
                    {nameof(ClosedService.Object), r => r.Object},
                    {nameof(ClosedService.CreatedDate), r => r.CreatedDate},
                    {nameof(ClosedService.UpdateDate), r => r.UpdateDate}
                };


                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                                    baseQuery.OrderBy(selectedColumn)
                                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var closedServices = baseQuery
                        .Skip(query.PageSize * (query.PageNumber - 1))
                        .Take(query.PageSize)
                        .ToList();

            var totalItemsCount = baseQuery.Count();

            var closedServicesDtos = _mapper.Map<List<ClosedServiceDto>>(closedServices);

            var result = new PagedResult<ClosedServiceDto>(closedServicesDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public void Update(int id, UpdateClosedServiceDto dto)
        {
            var closedService = _dbContext
                .ClosedServices
                .FirstOrDefault(x => x.Id == id);

            if (closedService is null)
            {
                throw new NotFoundException("Service not found");
            }


            closedService.UpdateInfo = dto.UpdateInfo;
            closedService.UpdateDate = dto.UpdateDate;
            _dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var message = $"ClosedService with id: {id} DELETE action invoked";

            _logger.LogError(message);


            var closedService = _dbContext
                .ClosedServices
                .FirstOrDefault(x => x.Id == id);

            if (closedService is null)
            {
                throw new NotFoundException("Service not found");
            }

            _dbContext.ClosedServices.Remove(closedService);
            _dbContext.SaveChanges();
        }
    }
}

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CA2254 // Template should be a static expression
