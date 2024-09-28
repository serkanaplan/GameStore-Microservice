using FilterService.Models;
using AutoMapper;
using Contracts;

namespace FilterService.MappingProfile;

public class BaseMapper : Profile
{
    public BaseMapper()
    {
        CreateMap<GameFilterItem,GameCreated>().ReverseMap();
    }
}