using AutoMapper;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Models;

namespace OgarnizerAPI
{
    public class OgarnizerMappingProfile : Profile
    {

        public OgarnizerMappingProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<Job, JobDto>();
                //.ForMember(r => r.CreatedDate,
                    //c => c.MapFrom(entity => new DateTimeOffset(entity.CreatedDate).ToUnixTimeMilliseconds()));

            CreateMap<Service, ServiceDto>();

            CreateMap<Order, OrderDto>();

            CreateMap<ClosedJob, ClosedJobDto>();

            CreateMap<ClosedService, ClosedServiceDto>();

            CreateMap<ClosedOrder, ClosedOrderDto>();

            CreateMap<CreateJobDto, Job>()
                .ForMember(r => r.CreatedDate,
                    c => c.MapFrom(dto => DateTime.Now));
        }
    }
}
