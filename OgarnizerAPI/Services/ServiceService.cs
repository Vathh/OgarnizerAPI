using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Exceptions;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;
using System.Linq.Expressions;

namespace OgarnizerAPI.Services
{
    public class ServiceService : IServiceService
    {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CA2254 // Template should be a static expression
        private readonly OgarnizerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<JobService> _logger;
        private readonly IUserContextService _userContextService;

        public ServiceService(OgarnizerDbContext dbContext,
                            IMapper mapper,
                            ILogger<JobService> logger,
                            IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
        }

        public int Create(CreateServiceDto dto)
        {
            var service = _mapper.Map<Service>(dto);
            _dbContext.Services.Add(service);
            _dbContext.SaveChanges();

            return service.Id;
        }
        public ServiceDto GetById(int id)
        {
            var service = _dbContext
                .Services
                .FirstOrDefault(x => x.Id == id);

            if (service is null)
            {
                throw new NotFoundException("Service not found");
            };

            var result = _mapper.Map<ServiceDto>(service);

            return result;
        }
        public PagedResult<ServiceDto> GetAll(ServiceQuery query)
        {
            var baseQuery = _dbContext
                            .Services
                            .Include(r => r.User)
                            .Where(r => query.SearchPhrase == null ||
                                        (r.Object.ToLower().Contains(query.SearchPhrase.ToLower()) || r.CreatedDate.Equals(query.SearchPhrase)));


            if (!string.IsNullOrEmpty(query.SortBy))
            {

                var columnsSelectors = new Dictionary<string, Expression<Func<Service, object>>>
                {
                    {nameof(Service.Object), r => r.Object},
                    {nameof(Service.CreatedDate), r => r.CreatedDate},
                    {nameof(Service.UpdateDate), r => r.UpdateDate}
                };


                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                                    baseQuery.OrderBy(selectedColumn)
                                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var services = baseQuery
                        .Skip(query.PageSize * (query.PageNumber - 1))
                        .Take(query.PageSize)
                        .ToList();

            var totalItemsCount = baseQuery.Count();

            var servicesDtos = _mapper.Map<List<ServiceDto>>(services);

            var result = new PagedResult<ServiceDto>(servicesDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }
        public void Update(int id, UpdateServiceDto dto)
        {
            var service = _dbContext
                .Services
                .FirstOrDefault(x => x.Id == id);

            if (service is null)
            {
                throw new NotFoundException("Service not found");
            }


            service.UpdateInfo = dto.UpdateInfo;
            service.UpdateDate = dto.UpdateDate;
            _dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var message = $"Service with id: {id} DELETE action invoked";

            _logger.LogError(message);


            var service = _dbContext
                .Services
                .FirstOrDefault(x => x.Id == id);

            if (service is null)
            {
                throw new NotFoundException("Job not found");
            }

            _dbContext.Services.Remove(service);
            _dbContext.SaveChanges();
        }
        public void Close(int id, bool isDone)
        {
            var service = _dbContext
                .Services
                .FirstOrDefault(x => x.Id == id);

            if (service is null)
            {
                throw new NotFoundException("Job not found");
            }

            var closedService = new ClosedService
            {
                UserId = service.UserId,
                CreatedDate = service.CreatedDate,
                Priority = service.Priority,
                Description = service.Description,
                Object = service.Object,
                AdditionalInfo = service.AdditionalInfo,
                UpdateInfo = service.UpdateInfo,
                IsDone = isDone,
                ClosedDate = DateTime.Now,
                CloseUserId = _userContextService.GetUserId
            };

            _dbContext.Services.Remove(service);
            _dbContext.ClosedServices.Add(closedService);
            _dbContext.SaveChanges();
        }
    }
}

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CA2254 // Template should be a static expression
