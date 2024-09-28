using GameService.Entities.Base;
using GameService.DTOs;

namespace GameService.Repositories.ForGame;

public interface IGameRepository
{
    Task<BaseResponseModel> CreateGame(GameDTO game);
    Task<BaseResponseModel> UpdateGame(UpdateGameDTO game, Guid gameId);
    Task<BaseResponseModel> RemoveGame(Guid gameId);
    Task<BaseResponseModel> GetAllGames();
    Task<BaseResponseModel> GetGamesByCategory(Guid categoryId);
    Task<BaseResponseModel> GetGameById(Guid gameId);
    Task<BaseResponseModel> GetMyGames();
}