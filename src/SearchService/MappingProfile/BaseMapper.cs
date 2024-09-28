using AutoMapper;
using Contracts;
using SearchService.Models;

namespace SearchService.MappingProfile;

public class BaseMapper : Profile
{
    public BaseMapper()
    {
        CreateMap<GameItem,GameCreated>().ReverseMap();
        CreateMap<GameItem,GameUpdated>().ReverseMap();
    }
}