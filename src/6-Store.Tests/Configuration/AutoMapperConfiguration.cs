using AutoMapper;
using Store.Domain.Entities;
using Store.Services.DTO;

namespace Store.Tests.Configurations.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static IMapper GetConfiguration()
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Costumer, CostumerDto>()
                    .ReverseMap();
                cfg.CreateMap<Order, OrderDto>()
                    .ReverseMap();
            });

            return autoMapperConfig.CreateMapper();
        }
    }
}