using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Exceptions;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;

namespace OgarnizerAPI.Services
{
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CA2254 // Template should be a static expression
    public class OrderService : IOrderService
    {
        private readonly OgarnizerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;
        private readonly IUserContextService _userContextService;

        public OrderService(OgarnizerDbContext dbContext,
                            IMapper mapper,
                            ILogger<OrderService> logger,
                            IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
        }
        public int Create(CreateOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return order.Id;
        }
        public OrderDto GetById(int id)
        {
            var order = _dbContext
                .Orders
                .FirstOrDefault(x => x.Id == id);

            if (order is null)
            {
                throw new NotFoundException("Order not found");
            };

            var result = _mapper.Map<OrderDto>(order);

            return result;
        }

        public PagedResult<OrderDto> GetAll(OrderQuery query)
        {

            var baseQuery = _dbContext
                            .Orders
                            .Include(r => r.User)
                            .Where(r => query.SearchPhrase == null ||
                                        (r.Client.ToLower().Contains(query.SearchPhrase.ToLower()) || r.CreatedDate.Equals(query.SearchPhrase)));


            if (!string.IsNullOrEmpty(query.SortBy))
            {

                var columnsSelectors = new Dictionary<string, Expression<Func<Order, object>>>
                {
                    {nameof(Order.Client), r => r.Client},
                    {nameof(Order.Object), r => r.Object},
                    {nameof(Order.CreatedDate), r => r.CreatedDate},
                    {nameof(Order.UpdateDate), r => r.UpdateDate}
                };


                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                                    baseQuery.OrderBy(selectedColumn)
                                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var orders = baseQuery
                        .Skip(query.PageSize * (query.PageNumber - 1))
                        .Take(query.PageSize)
                        .ToList();

            var totalItemsCount = baseQuery.Count();

            var ordersDtos = _mapper.Map<List<OrderDto>>(orders);

            var result = new PagedResult<OrderDto>(ordersDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }
        public void Update(int id, UpdateOrderDto dto)
        {

            var order = _dbContext
                .Orders
                .FirstOrDefault(x => x.Id == id);

            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }


            order.UpdateInfo = dto.UpdateInfo;
            order.UpdateDate = dto.UpdateDate;
            _dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var message = $"Order with id: {id} DELETE action invoked";

            _logger.LogError(message);


            var order = _dbContext
                .Orders
                .FirstOrDefault(x => x.Id == id);

            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }

            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
        }
        public void Close(int id, bool isDone)
        {
            var order = _dbContext
                .Orders
                .FirstOrDefault(x => x.Id == id);

            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }

            var closedOrder = new ClosedOrder
            {
                UserId = order.UserId,
                CreatedDate = order.CreatedDate,
                Priority = order.Priority,
                Description = order.Description,
                Client = order.Client,
                Object = order.Object,
                AdditionalInfo = order.AdditionalInfo,
                UpdateInfo = order.UpdateInfo,
                IsDone = isDone,
                ClosedDate = DateTime.Now,
                CloseUserId = _userContextService.GetUserId
            };

            _dbContext.Orders.Remove(order);
            _dbContext.ClosedOrders.Add(closedOrder);
            _dbContext.SaveChanges();
        }
    }
}

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CA2254 // Template should be a static expression
