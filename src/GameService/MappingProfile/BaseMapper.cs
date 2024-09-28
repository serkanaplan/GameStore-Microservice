using AutoMapper;
using Contracts;
using GameService.DTOs;
using GameService.Entities;

namespace GameService.MappingProfile;

public class BaseMapper : Profile
{
    public BaseMapper()
    {
        CreateMap<Category,CategoryDTO>().ReverseMap();
        CreateMap<Game,GameDTO>().ReverseMap();
        CreateMap<GameDTO,GameCreated>().ReverseMap();
        CreateMap<Game,GameCreated>().ReverseMap();
        CreateMap<Game,GameUpdated>().ReverseMap();
        CreateMap<MyGame,MyGameDTO>().ReverseMap();
    }
}