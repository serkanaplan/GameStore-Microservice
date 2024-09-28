using AutoMapper;
using BasketService.Model;
using Contracts;

namespace BasketService.MappingProfile;

public class BaseMapper : Profile
{
    public BaseMapper()
    {
        CreateMap<Checkout,CheckoutBasketModel>().ReverseMap();
    }
}