using AutoMapper;
using Contracts;
using OrderService.Entities;

namespace OrderService.MappingProfile;

public class BaseMapper : Profile
{
    public BaseMapper()
    {
        CreateMap<CheckoutBasketModel,Order>().ReverseMap();
    }
}