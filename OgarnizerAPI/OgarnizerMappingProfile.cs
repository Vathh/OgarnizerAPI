using AutoMapper;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Models;

namespace OgarnizerAPI
{
    public class OgarnizerMappingProfile : Profile
    {

        public OgarnizerMappingProfile()
        {
            #region User
            CreateMap<User, UserDto>();
            #endregion

            #region Job
            CreateMap<Job, JobDto>();
            //.ForMember(r => r.CreatedDate,
            //c => c.MapFrom(entity => new DateTimeOffset(entity.CreatedDate).ToUnixTimeMilliseconds()));

            CreateMap<CreateJobDto, Job>()
                .ForMember(r => r.CreatedDate,
                    c => c.MapFrom(dto => DateTime.Now));
            #endregion

            #region ClosedJob
            CreateMap<ClosedJob, ClosedJobDto>();
            #endregion

            #region Service
            CreateMap<Service, ServiceDto>();

            CreateMap<CreateServiceDto, Service>()
                .ForMember(r => r.CreatedDate,
                    c => c.MapFrom(dto => DateTime.Now));
            #endregion

            #region ClosedService
            CreateMap<ClosedService, ClosedServiceDto>();
            #endregion

            #region Order
            CreateMap<Order, OrderDto>();

            CreateMap<CreateOrderDto, Order>()
                .ForMember(r => r.CreatedDate,
                    c => c.MapFrom(dto => DateTime.Now));
            #endregion

            #region ClosedOrder
            CreateMap<ClosedOrder, ClosedOrderDto>();
            #endregion



        }
    }
}
