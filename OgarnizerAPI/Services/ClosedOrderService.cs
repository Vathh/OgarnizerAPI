using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Exceptions;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;

namespace OgarnizerAPI.Services
{
    public class ClosedOrderService : IClosedOrderService
    {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CA2254 // Template should be a static expression
        private readonly OgarnizerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ClosedOrderService> _logger;
        private readonly IUserContextService _userContextService;

        public ClosedOrderService(OgarnizerDbContext dbContext,
                                IMapper mapper,
                                ILogger<ClosedOrderService> logger,
                                IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
        }

        public ClosedOrderDto? GetById(int id)
        {
            var closedOrder = _dbContext
                .ClosedOrders
                .FirstOrDefault(x => x.Id == id);

            if (closedOrder == null)
            {
                throw new NotFoundException("ClosedOrder not found");
            };

            var result = _mapper.Map<ClosedOrderDto>(closedOrder);

            return result;
        }

        public PagedResult<ClosedOrderDto> GetAll(ClosedOrderQuery query)
        {
            var baseQuery = _dbContext
                            .ClosedOrders
                            .Include(r => r.CloseUser)
                            .Where(r => query.SearchPhrase == null ||
                                    (r.Client.ToLower().Contains(query.SearchPhrase.ToLower()) || r.CreatedDate.Equals(query.SearchPhrase)));

            if (!string.IsNullOrEmpty(query.SortBy))
            {

                var columnsSelectors = new Dictionary<string, Expression<Func<ClosedOrder, object>>>
                {
                    {nameof(ClosedOrder.Client), r => r.Client},
                    {nameof(ClosedOrder.Object), r => r.Object},
                    {nameof(ClosedOrder.CreatedDate), r => r.CreatedDate},
                    {nameof(ClosedOrder.UpdateDate), r => r.UpdateDate}
                };


                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                                    baseQuery.OrderBy(selectedColumn)
                                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var closedOrders = baseQuery
                        .Skip(query.PageSize * (query.PageNumber - 1))
                        .Take(query.PageSize)
                        .ToList();

            var totalItemsCount = baseQuery.Count();

            var closedOrdersDtos = _mapper.Map<List<ClosedOrderDto>>(closedOrders);

            var result = new PagedResult<ClosedOrderDto>(closedOrdersDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public void Update(int id, UpdateClosedOrderDto dto)
        {
            var closedOrder = _dbContext
                .ClosedOrders
                .FirstOrDefault(x => x.Id == id);

            if (closedOrder is null)
            {
                throw new NotFoundException("Order not found");
            }


            closedOrder.UpdateInfo = dto.UpdateInfo;
            closedOrder.UpdateDate = dto.UpdateDate;
            _dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var message = $"ClosedOrder with id: {id} DELETE action invoked";

            _logger.LogError(message);


            var closedOrder = _dbContext
                .ClosedOrders
                .FirstOrDefault(x => x.Id == id);

            if (closedOrder is null)
            {
                throw new NotFoundException("Order not found");
            }

            _dbContext.ClosedOrders.Remove(closedOrder);
            _dbContext.SaveChanges();
        }
    }
}

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CA2254 // Template should be a static expression
